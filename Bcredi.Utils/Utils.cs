using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;

namespace Bcredi.Utils
{
    public class Utils
    {
        public static String SMTP_HOST = ConfigurationManager.AppSettings["smtpHost"];
        public static String SMTP_PORT = ConfigurationManager.AppSettings["port"];
        public static String EMAIL_PASSWORD = ConfigurationManager.AppSettings["emailPassword"];
        public static String EMAIL_FROM = ConfigurationManager.AppSettings["EmailFrom"];
        public static String CAMINHO_DOCUMENTO = ConfigurationManager.AppSettings["CaminhoDocumento"];

        public static string Serialize<T>(T obj)
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

        public static string Encryption(string strText)
        {
            //TODO Marcelo: Gerar as chaves publica e privada e gravar na base de dados
            var publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var dados = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    //Criptografar a senha com a chave publica
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(dados, true);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string Decryption(string strText)
        {
            var privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";
            var dados = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;

                    // Descriptografar a senha com a chave privada
                    rsa.FromXmlString(privateKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string CreatePassword()
        {
            const string SenhaCaracteresValidos = "abcdefghijklmnopqrstuvwxyz1234567890";
            const string SenhaCaracteresEspeciais = "!@#$%¨&*()_+";

            //Aqui eu defino o número de caracteres que a senha terá
            int tamanho = 8;

            //Aqui pego o valor máximo de caracteres para gerar a senha
            int valormaximo = SenhaCaracteresValidos.Length;

            int valormaximoCaracteresEspeciais = SenhaCaracteresEspeciais.Length;

            //Criamos um objeto do tipo randon
            Random random = new Random(DateTime.Now.Millisecond);

            //Criamos a string que montaremos a senha
            StringBuilder senha = new StringBuilder(tamanho);

            //Fazemos um for adicionando os caracteres a senha
            for (int i = 0; i < tamanho - 2; i++)
            {
                senha.Append(SenhaCaracteresValidos[random.Next(0, valormaximo)]);
            }

            for (int i = 0; i < 2; i++)
            {
                senha.Append(SenhaCaracteresEspeciais[random.Next(0, valormaximoCaracteresEspeciais)]);
            }

            //retorna a senha
            return senha.ToString();
        }


        public static void EnviarEmail(string emailRemetente, string emailDestinatario, string assunto, string msg)
        {
            //Cria o objeto que envia o e-mail
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "10.25.1.254";
            client.EnableSsl = false;
            //client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(emailRemetente, "b@radm1n");

            //Cria o endereço de email do remetente
            MailAddress de = new MailAddress(emailRemetente);
            //Cria o endereço de email do destinatário -->
            MailAddress para = new MailAddress(emailDestinatario);
            MailMessage mensagem = new MailMessage(de, para);
            mensagem.IsBodyHtml = true;
            //Assunto do email
            mensagem.Subject = assunto;
            //Conteúdo do email
            mensagem.Body = msg;
            try
            {
                //Envia o email
                client.Send(mensagem);
            }
            catch (Exception excessao)
            {
                //TODO Labbati: Logar o erro
            }
        }

        public static string clearCaracteresEspeciais(string campo)
        {
            campo = Regex.Replace(campo, "[^0-9a-zA-Z]+", "");
            return campo;
        }

        /// <summary>
        /// Recuperar o ip do cliente
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Ip do cliente</returns>
        public static string getUserIp(HttpRequestBase request)
        {
            string ipCliente = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.ServerVariables["REMOTE_ADDR"];

            if (ipCliente.Equals("::1"))
            {
                ipCliente = Utils.GetLocalIPAddress();
            }

            return ipCliente;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Endereço IP não encontrado!");
        }

        /// <summary>
        /// Ler o conteudo de um arquivo e retornar todo o texto
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <returns>Conteúdo do arquivo</returns>
        public static string readFileToString(string filePath)
        {

            string lineOfText = "";
            StringBuilder textContent = new StringBuilder();

            var filestream = new System.IO.FileStream(filePath,
                                          System.IO.FileMode.Open,
                                          System.IO.FileAccess.Read,
                                          System.IO.FileShare.ReadWrite);
            var file = new System.IO.StreamReader(filestream, System.Text.Encoding.UTF8, true, 128);

            while ((lineOfText = file.ReadLine()) != null)
            {
                textContent.Append(lineOfText);
            }

            return textContent.ToString();
        }

        /// <summary>
        /// Recuperar o MimeType do arquivo 
        /// </summary>
        /// <param name="fileName">Caminho do arquivo</param>
        /// <returns>MimeType</returns>
        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        /// <summary>
        /// Ler um arquivo a partir do disco
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <returns></returns>
        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public static String GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        /// <summary>
            /// Formatar uma string CNPJ
            /// </summary>
            /// <param name="CNPJ">string CNPJ sem formatacao</param>
            /// <returns>string CNPJ formatada</returns>
            /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>

        public static string FormatCNPJ(string CNPJ)
        {
            if (string.IsNullOrEmpty(CNPJ))
            {
                return "";
            }
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string limparMascara(string valor)
        {
            Regex r = new Regex(@"\d+");
            string resultado = "";
            foreach (Match m in r.Matches(valor))
            {
                resultado += m.Value;
            }

            return resultado;
        }

        /// <summary>
        /// Formatar o cpf ou cnpj com base no tamanho da string
        /// </summary>
        /// <param name="paramCpfCnpj">Campo a ser formatado</param>
        /// <returns>campo formatado com a mascara correspondente</returns>
        public static string FormataCPFCNPJ(string paramCpfCnpj) {

            string cpfCNPJ = "";

            if (paramCpfCnpj.Length == 11)
            {
                cpfCNPJ = Bcredi.Utils.Utils.FormatCPF(paramCpfCnpj);
            }
            else
            {
                cpfCNPJ = Bcredi.Utils.Utils.FormatCNPJ(paramCpfCnpj);
            }

            return cpfCNPJ;
        }

        /// <summary>
            /// Formatar uma string CPF
            /// </summary>
            /// <param name="CPF">string CPF sem formatacao</param>
            /// <returns>string CPF formatada</returns>
            /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>

        public static string FormatCPF(string CPF)
        {
            if (string.IsNullOrEmpty(CPF))
            {
                return "";
            }
            try
            {
                CPF = Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
            }
            catch (Exception e)
            {
                CPF = string.Empty;
            }
            return CPF;
        }

        /// <summary>
        /// Recuperar o dia util anterior
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime PreviousWorkDay(DateTime date)
        {
            do
            {
                date = date.AddDays(-1);
            }
            while (IsHoliday(date) || IsWeekend(date));

            return date;
        }

        //TODO LABBATI Consultar na tabela da prognum os feriados
        private bool IsHoliday(DateTime date)
        {
            return false;
        }

        /// <summary>
        /// Verificar se a data é um final de semana
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns></returns>
        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        /**
        * DateDiff para C#
        * Indica qual será o retorno [d = Dias, m = Meses, y = Anos]
        * Data Inicial
        * Data Final
        * Retorna a diferença de acordo com o Intervalo escolhido
        */
        public static int DateDiff(char charInterval, DateTime dttFromDate, DateTime dttToDate)
        {
            TimeSpan tsDuration;
            tsDuration = dttToDate - dttFromDate;

            if (charInterval == 'd')
            {
                // Resultado em Dias
                return tsDuration.Days;
            }
            else if (charInterval == 'm')
            {
                // Resultado em Meses
                double dblValue = 12 * (dttFromDate.Year - dttToDate.Year) + dttFromDate.Month - dttToDate.Month;
                return Convert.ToInt32(Math.Abs(dblValue));
            }
            else if (charInterval == 'y')
            {
                // Resultado em Anos
                return Convert.ToInt32((tsDuration.Days) / 365);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
            /// Retira a Formatacao de uma string CNPJ/CPF
            /// </summary>
            /// <param name="Codigo">string Codigo Formatada</param>
            /// <returns>string sem formatacao</returns>
            /// <example>Recebe '99.999.999/9999-99' Devolve '99999999999999'</example>

        public static string SemFormatacao(string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }

        // o metodo isCPFCNPJ recebe dois parâmetros:
        // uma string contendo o cpf ou cnpj a ser validado
        // e um valor do tipo boolean, indicando se o método irá
        // considerar como válido um cpf ou cnpj em branco.
        // o retorno do método também é do tipo boolean:
        // (true = cpf ou cnpj válido; false = cpf ou cnpj inválido)
        public static bool isCPFCNPJ(string cpfcnpj, bool isCpfCNPJBrancoValido)
        {
            if (string.IsNullOrEmpty(cpfcnpj))
                return isCpfCNPJBrancoValido;
            else
            {
                int[] d = new int[14];
                int[] v = new int[2];
                int j, i, soma;
                string Sequencia, SoNumero;

                SoNumero = Regex.Replace(cpfcnpj, "[^0-9]", string.Empty);

                //verificando se todos os numeros são iguais
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;

                // se a quantidade de dígitos numérios for igual a 11
                // iremos verificar como CPF
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }
                // se a quantidade de dígitos numérios for igual a 14
                // iremos verificar como CNPJ
                else if (SoNumero.Length == 14)
                {
                    Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
                }
                // CPF ou CNPJ inválido se
                // a quantidade de dígitos numérios for diferente de 11 e 14
                else return false;
            }
        }
    }
}
