using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Timers;

namespace Soldador
{
    public partial class Form1 : Form
    {
        public int clientID;
        public int st = 0;
        public float[,] PP = new float[100, 4];
        public int nl = 0;
        public int pcount = 0;
        public int dly = 0;
        public int mov = 0;

        /// <summary>
        /// ///////////////////////////////////////////NOVAS VARIAVEIS///////////////////////////////////
        public int portNb = 19997;
        //Pos Elemento Final
        public double Px;
        public double Py;
        public double Pz;
        //pos juntas
        public float J1 = 0;
        public float J2 = 0;
        public float J3 = 0;
        public float[] pDummy = new float[3];
        //handles
        public int Junta1;
        public int Junta2;
        public int Junta3;
        public int Dummy;

        //Arruma o TEXTO
        double posicaoDoEspaco;
        int nlinha;
        int continua;
        string antes;
        string depois;

        //Verifica Regras Basicas AML
        int tamanho;
        int condicao = 0;
        int condTotal;
        int nSub = 0;
        int nEnd = 0;

        //Interpretador
        public List<string> varConstS = new List<string>();
        public List<string> varPontoS = new List<string>();
        int tamBilu = 0;
        public float Dtempo = 0;

        //Necessarios para Manipulação de variaveis
        string Xtira1;
        string Xtira2;
        string Ytira1;
        string Ytira2;
        string Texto1;
        string Texto2;
        
        //novo
        public int nMov = 0;
        /// 
        /// </summary>



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clientID = VREPWrapper.simxStart("127.0.0.1", portNb, true, true, 500, 5);
            Thread.Sleep(100);
            if (clientID > -1)
            {
                Status.Text = "Status: Conectado";
            }
            else
            {
                Status.Text = "Status: Não Conectado";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (clientID > -1)
            {
                VREPWrapper.simxFinish(clientID);
            }
        }

        private void BConnect_Click(object sender, EventArgs e)
        {
            if (clientID > -1)
            {
                if (st == 0)
                {
                    VREPWrapper.simxSynchronous(clientID, true);
                    Thread.Sleep(100);
                    VREPWrapper.simxStartSimulation(clientID, simx_opmode.oneshot);
                    VREPWrapper.simxSetIntegerSignal(clientID, "opp", 0, simx_opmode.oneshot);
                    BConnect.Text = "Stop";
                    st = 1;
                    Timer.Enabled = true;
                }
                else
                {
                    VREPWrapper.simxStopSimulation(clientID, simx_opmode.oneshot);
                    Thread.Sleep(100);
                    BConnect.Text = "Start";
                    st = 0;
                    Timer.Enabled = false;
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (clientID > -1)
            {
                VREPWrapper.simxGetIntegerSignal(clientID, "Status", ref mov, simx_opmode.oneshot);
                if (mov == 1)
                { MM.Checked = true; }
                else
                { MM.Checked = false; }
                if (dly > 0)
                {
                    dly = dly - 1;
                }
                VREPWrapper.simxSynchronousTrigger(clientID);
                Thread.Sleep(10);
            }
        }

        //Código de Teste
        private void CBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int op = CBox.SelectedIndex;
            if ((op == 0) || (op == 4))
            {
                Xval.Enabled = false;
                Zval.Enabled = false;
            }
            else if (op == 3)
            {
                Xval.Enabled = false;
                Zval.Enabled = true;
            }
            else
            {
                Xval.Enabled = true;
                Zval.Enabled = true;
            }
        }

        private void Xval_Leave(object sender, EventArgs e)
        {
            if (Xval.Text.Contains("."))
            {
                Xval.Text = Xval.Text.Replace(".", ",");
            }
        }

        private void Zval_Leave(object sender, EventArgs e)
        {
            if (Zval.Text.Contains("."))
            {
                Zval.Text = Zval.Text.Replace(".", ",");
            }
        }

        private void BProg_Click(object sender, EventArgs e)
        {
            int opp = CBox.SelectedIndex;
            if (clientID > -1)
            {
                VREPWrapper.simxSetIntegerSignal(clientID, "opp", opp, simx_opmode.oneshot);
                if ((opp == 1) || (opp == 2))
                {
                    float X = float.Parse(Xval.Text);
                    float Z = float.Parse(Zval.Text);
                    VREPWrapper.simxSetFloatSignal(clientID, "Xx", X, simx_opmode.oneshot);
                    VREPWrapper.simxSetFloatSignal(clientID, "Yy", Z, simx_opmode.oneshot);
                }
                if (opp == 3)
                {
                    float Z = float.Parse(Zval.Text);
                    VREPWrapper.simxSetFloatSignal(clientID, "Zz", Z, simx_opmode.oneshot);
                }
                if (opp == 4) { dly = 20; } // 20*50ms = 1s
            }
        }
        //Fim do codigo de teste


        //Roda codigo da caixa de texto
        private void Roda_Click(object sender, EventArgs e)
        {
            //Zerar todas as variaveis
            nMov = 0;
            //pega o total de linhas
            nlinha = richTextBox1.Lines.Count();
            //numberLabel.Text = lineCount.ToString();
            var lines = this.richTextBox1.Text.Split('\n').ToList();

            //Para remover os espaços do texto e Case sensitivity
            for (int i = 0; i < nlinha; i++)
            {
                continua = 1;
                while (continua == 1)
                {
                    posicaoDoEspaco = lines[i].IndexOf(" ");
                    if (posicaoDoEspaco > -1)
                    {
                        antes = lines[i].Remove((int)posicaoDoEspaco);
                        depois = lines[i].Remove(0, (int)posicaoDoEspaco + 1);
                        lines[i] = (antes + depois);
                    }


                    if (lines[i].IndexOf(" ") < 0)
                    {
                        continua = 0;
                        string aaa = lines[i];
                        //aaa = aaa;
                    }

                    //Tira case sensitivity e deixa tudo minusculo
                    lines[i] = lines[i].ToLower();
                    string asda9sufnisa = lines[i];
                }
            }

            condicao = 0;
            //Verifica os requisitos de AML
            for (int i = 0; i < nlinha; i++)
            {
                //Verifica se a linha é vazia
                if ((string.IsNullOrWhiteSpace(lines[i]) || lines[i].Trim().Length == 0))
                {
                    condTotal = condTotal + 1;
                }
                else
                {
                    tamanho = lines[i].Length;
                    //Toda linha termina com ";";
                    if (lines[i].LastIndexOf(";") != tamanho - 1)
                    {
                        // Deve terminar com ";"
                        //Colocar as mensagens todas no mesmo lugar e deixar invisível ou mandar escrever a mensagem?
                        //Mandar escrever a mensagem, mas deixar o texto invisível quando não for necessário.
                    }
                    else
                    {
                        condicao = condicao + 1;
                    }
                    //Até 72 caracteres;
                    if (tamanho > 72)
                    {
                        // até 72 caracteres ;
                    }
                    else
                    {
                        condicao = condicao + 1;
                    }
                    //O primeiro caractere deve ser alfabético, os demais caracteres podem ser alfabéticos, numéricos ou o caractere sublinhado(_)
                    if (Regex.IsMatch(lines[i].Substring(0, 1), "^[0 - 9]") == true)
                    {
                        // O primeiro caractere deve ser alfabético ;
                    }
                    else
                    {
                        condicao = condicao + 1;
                    }
                    //O caractere sublinhado(_) não pode ser o último caractere do identificador;
                    if (lines[i].LastIndexOf("_") == tamanho - 2)
                    {
                        //Ultimo não pode ser _
                    }
                    else
                    {
                        condicao = condicao + 1;
                    }
                    //Caracteres especiais, como o asterisco, não são permitidos.
                    if (Regex.IsMatch(lines[i].Substring(0, tamanho), "^[a-z:;_().,-]*") == true)
                    {
                        condicao = condicao + 1;
                    }
                    else
                    {
                        //n permitido caracter especial
                    }
                    //SUbr = End
                    if (lines[i].IndexOf(":subr") > -1)
                    {
                        nSub = nSub + 1;
                    }
                    else if (lines[i].IndexOf("end") > -1)
                    {
                        nEnd = nEnd + 1;
                    }


                    if (condicao == 5)
                    {
                        condTotal = condTotal + 1;
                    }
                    condicao = 0;
                }
            }

            if (nSub != nEnd)
            {
                //emitir mensagem que faltou fechar as Subrotinas
            }

            float[] varPontoX = new float[nlinha]; //maximo de variaveis sera no max igual ao numero de linhas
            float[] varPontoY = new float[nlinha];

            //Se esta nas regras do AML começa a interpretar o código
            if ((condTotal == nlinha) & (nSub == nEnd))
            {
                for (int i = 0; i < nlinha; i++)
                {
                    //Verifica se não é comentário
                    if (lines[i].IndexOf("--") != 0)
                    {
                        //Declara variáveis NOVO ponto ou NOVA constante
                        if (lines[i].IndexOf(":newpt") >= 0)
                        {
                            //recebe tudo que estiver antes dos : do new pt que é o nome da variavel
                            varPontoS.Add(lines[i].Substring(0, (int)lines[i].IndexOf(":")));
                            //valores dos eixos x, y e z.
                            Xtira1 = lines[i].Remove(0, (int)lines[i].IndexOf("(") + 1);
                            Xtira2 = Xtira1.Remove((int)Xtira1.IndexOf(","));
                            varPontoX[tamBilu] = float.Parse(Xtira2, CultureInfo.InvariantCulture.NumberFormat);//pega o valor depois de PT( e a ,
                            Ytira1 = lines[i].Remove(0, (int)lines[i].IndexOf(",") + 1);
                            Ytira2 = Ytira1.Remove((int)Ytira1.IndexOf(")"));
                            varPontoY[tamBilu] = float.Parse(Ytira2, CultureInfo.InvariantCulture.NumberFormat);//pega o valor depois da virgula e entre outra virgula
                            //conta o numero de constantes declaradas
                            tamBilu = tamBilu + 1;
                        }
                        /*else if (lines[i].IndexOf(":newn") > 0)
                        {
                            //pega uma constante em string
                            varConstS[i] = lines[i].Substring(0, lines[i].IndexOf(":")); // sera que é index -1?
                            // valor da constante
                            Const1 = lines[i].Remove(0, (int)lines[i].IndexOf("w") + 2);
                            Const2 = Const1.Remove((int)lines[i].IndexOf(";"));
                            varConst[i] = Convert.ToInt32(Const2); //pega o valor que sobrou
                            //conta o numero de constantes declaradas
                            tamBilu = tamBilu + 1;
                        }*/
                        else if ((lines[i].IndexOf(":n") > 0) & (lines[i].IndexOf(":newpt") < 0) & (lines[i].IndexOf(":newn") < 0))
                        {
                            // Você quis dizer :newPT ou :newn
                        }
                        else
                        {
                            //verifica se é um movimento absoluto direto
                            if ((lines[i].IndexOf("pmove") >= 0))
                            {
                                if ((lines[i].IndexOf("pt") >= 0))
                                {
                                    Xtira1 = lines[i].Remove(0, (int)lines[i].IndexOf("(") + 1);
                                    Xtira1 = Xtira1.Remove(0, (int)Xtira1.IndexOf("(") + 1);
                                    Xtira2 = Xtira1.Remove((int)Xtira1.IndexOf(","));
                                    float numeroX = float.Parse(Xtira2, CultureInfo.InvariantCulture.NumberFormat);//pega o valor depois de PT( e a ,
                                    Ytira1 = lines[i].Remove(0, (int)lines[i].IndexOf(",") + 1);
                                    Ytira2 = Ytira1.Remove((int)Ytira1.IndexOf(")"));
                                    float numeroY = float.Parse(Ytira2, CultureInfo.InvariantCulture.NumberFormat);//pega o valor depois da virgula e entre outra virgula

                                    //Carrega programação na matriz (X e Y)
                                    PP[nMov, 0] = 2;
                                    if ((PP[nMov, 0] == 1) || (PP[nMov, 0] == 2) || (PP[nMov, 0] == 3))
                                    {
                                        if (PP[nMov, 0] != 3)
                                        {
                                            PP[nMov, 1] = numeroX;
                                        }
                                        PP[nMov, 2] = numeroY;
                                        nMov = nMov + 1;
                                    }

                                }
                                //vai mover através de uma variavel pt
                                else
                                {
                                    //remove a constante do resto do texto
                                    Texto1 = lines[i].Remove(0, (int)lines[i].IndexOf("(") + 1);
                                    Texto2 = Texto1.Remove((int)Texto1.IndexOf(")")); //pega a string depois de pmove( e )
                                    //pega o valor da constante correta
                                    for (int bilu = 0; bilu < tamBilu; bilu++)
                                    {
                                        if (varPontoS[bilu] == Texto2)
                                        {
                                            //Carrega programação na matriz (X e Y)
                                            PP[nMov, 0] = 2;
                                            if ((PP[nMov, 0] == 1) || (PP[nMov, 0] == 2) || (PP[nMov, 0] == 3))
                                            {
                                                if (PP[nMov, 0] != 3)
                                                {
                                                    PP[nMov, 1] = varPontoX[bilu];
                                                }
                                                PP[nMov, 2] = varPontoY[bilu];
                                                nMov = nMov + 1;
                                            }

                                        }
                                    }
                                }
                            }

                            //verifica se é um movimento de descer completamente o eixo Z
                            //OK - Só falta colocar a verificação de que o movimento chegou
                            if ((lines[i].IndexOf("down") >= 0))
                            {
                                //Carrega programação na matriz (Z)
                                PP[nMov, 0] = 3;
                                if ((PP[nMov, 0] == 3))
                                {
                                    PP[nMov, 2] = (float)0;
                                    nMov = nMov + 1;
                                }

                            }

                            //Verifica se é um movimento de subir completamente o eixo Z
                            //OK - So falta pegar a info que chegou a POS FIN
                            if ((lines[i].IndexOf("up") >= 0))
                            {
                                //Carrega programação na matriz (Z)
                                PP[nMov, 0] = 3;
                                if ((PP[nMov, 0] == 3))
                                {
                                    PP[nMov, 2] = (float)(0.2357);
                                    nMov = nMov + 1;
                                }
                            }
                            
                            //verifica se é um delay entre movimentos
                            if ((lines[i].IndexOf("delay") >= 0))
                            {
                                //pega o tempo do delay
                                Texto1 = lines[i].Remove(0, (int)lines[i].IndexOf("(") + 1);
                                Texto2 = Texto1.Remove((int)Texto1.IndexOf(")")); //pega a string depois de pmove( e )
                                Dtempo = float.Parse(Texto2, CultureInfo.InvariantCulture.NumberFormat);

                                //Carrega programação na matriz (delay)
                                PP[nMov, 0] = 4;
                                if ((PP[nMov, 0] == 4))
                                {
                                    PP[nMov, 3] = Dtempo;
                                }
                                nMov = nMov + 1;
                            }

                            

                        }

                    }
                }

                //Conta quantas linhas
                /*for (int i = 0; i < nMov; i++)
                {
                    if (PP[i, 0] != 0f) nl = nl + 1;
                }*/
                pcount = 0;
                Roda.Text = "Rodando";
                Matriz.Text = "";
                TProg.Enabled = true;
                for (int i=0;i<nMov;i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Matriz.Text = Matriz.Text + PP[i, j] + " ";
                    }
                    Matriz.Text = Matriz.Text + "|\n";
                }
                condTotal = 0;
                nSub = 0;
                nEnd = 0;
                tamBilu = 0;
            }

        }

        private void TProg_Tick(object sender, EventArgs e)
        {
            if (nMov > pcount)
            {
                Roda.Text = String.Join("Rodando - ", Convert.ToString(pcount));
                if ((mov == 0) && (dly == 0))
                {
                    int opp = Convert.ToInt32(PP[pcount, 0]);
                    VREPWrapper.simxSetIntegerSignal(clientID, "opp", Convert.ToInt32(PP[pcount, 0]), simx_opmode.oneshot);
                    if ((opp == 1) || (opp == 2))
                    {
                        VREPWrapper.simxSetFloatSignal(clientID, "Xx", PP[pcount, 1], simx_opmode.oneshot);
                        VREPWrapper.simxSetFloatSignal(clientID, "Yy", PP[pcount, 2], simx_opmode.oneshot);
                    }
                    if (opp == 3)
                    {
                        VREPWrapper.simxSetFloatSignal(clientID, "Zz", PP[pcount, 2], simx_opmode.oneshot);
                    }
                    if (opp == 4)
                    {
                        dly = 20* (int)PP[pcount, 3];
                    }
                    pcount = pcount + 1;
                }
            }
            else
            {
                if ((mov == 0) && (dly == 0))
                {
                    nMov = 0;
                    pcount = 0;
                    Roda.Text = "Roda";
                    TProg.Enabled = false;
                }
            }
        }

        private void Matriz_Click(object sender, EventArgs e)
        {
            Matriz.Text = " ";
        }
    }
}

