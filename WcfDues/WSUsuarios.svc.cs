using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WSUsuarios" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WSUsuarios.svc or WSUsuarios.svc.cs at the Solution Explorer and start debugging.
    public class WSUsuarios : WsBase, IWSUsuarios
    {
        //MDVR
        SocketPermission permission;
        Socket sListener;
        IPEndPoint ipEndPoint;
        Socket handler;

        bool bandera_video;
        public string video()
        {
            try
            {
                LivePreviewRequest();

                return "video ok";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string socket()
        {
            try
            {

                //  ConnectarServerStreaming();

                // Creates one SocketPermission object for access restrictions
                permission = new SocketPermission(
                NetworkAccess.Accept,     // Allowed to accept connections 
                TransportType.Tcp,        // Defines transport types 
                "",                       // The IP addresses of local host 
                SocketPermission.AllPorts // Specifies all ports 
                );

                // Listening Socket object 
                sListener = null;


                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Resolves a host name to an IPHostEntry instance 
                IPHostEntry ipHost = Dns.GetHostEntry("");

                // Gets first IP address associated with a localhost 
                IPAddress ipAddr = ipHost.AddressList[0];

                // Creates a network endpoint 
                //ipEndPoint = new IPEndPoint(ipAddr, 4510);
                //ipEndPoint = new IPEndPoint(IPAddress.Any, 6608);
                ipEndPoint = new IPEndPoint(IPAddress.Parse("172.16.5.78"), 6608);
                //ipEndPoint = new IPEndPoint(IPAddress.Parse(txtip.Text), 5000);


                // Create one Socket object to listen the incoming connection 
                /*sListener = new Socket(
                    ipAddr.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp
                    );*/

                sListener = new Socket(
                 AddressFamily.InterNetwork,
                 SocketType.Stream,
                 ProtocolType.Tcp
                 );


                // Associates a Socket with a local endpoint 
                sListener.Bind(ipEndPoint);


                /*tbStatus.Text = "Server started.";

                Start_Button.IsEnabled = false;
                StartListen_Button.IsEnabled = true;*/

                // Places a Socket in a listening state and specifies the maximum 
                // Length of the pending connections queue 
                sListener.Listen(10);


                // Begins an asynchronous operation to accept an attempt 
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                sListener.BeginAccept(aCallback, sListener);


                return "ok";

                /*tbStatus.Text = "Server is now listening on " + ipEndPoint.Address + " port: " + ipEndPoint.Port;

                StartListen_Button.IsEnabled = false;
                Send_Button.IsEnabled = true;*/
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.ToString());
                return ex.Message;
            }
        }
        public void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = null;

            // A new Socket to handle remote host communication 
            Socket handler = null;
            try
            {
                // Receiving byte array 
                byte[] buffer = new byte[1024];
                //byte[] buffer = new byte[9024];
                // Get Listening Socket object 
                listener = (Socket)ar.AsyncState;
                // Create a new socket 
                handler = listener.EndAccept(ar);

                // Using the Nagle algorithm 
                handler.NoDelay = false;

                // Creates one object array for passing data 
                object[] obj = new object[2];
                obj[0] = buffer;
                obj[1] = handler;

                // Begins to asynchronously receive data 
                handler.BeginReceive(
                    buffer,        // An array of type Byt for received data 
                    0,             // The zero-based position in the buffer  
                    buffer.Length, // The number of bytes to receive 
                    SocketFlags.None,// Specifies send and receive behaviors 
                    new AsyncCallback(ReceiveCallback),//An AsyncCallback delegate 
                    obj            // Specifies infomation for receive operation 
                    );

                // Begins an asynchronous operation to accept an attempt 
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                listener.BeginAccept(aCallback, listener);
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }
        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Fetch a user-defined object that c+ontains information 
                object[] obj = new object[2];
                obj = (object[])ar.AsyncState;

                // Received byte array 
                byte[] buffer = (byte[])obj[0];

                //Obtenemos el numero de mensaje
                string NMensaje = buffer[3].ToString("X2") + buffer[2].ToString("X2");

                // A Socket to handle remote host communication. 
                handler = (Socket)obj[1];

                // The number of bytes received. 
                int bytesRead = handler.EndReceive(ar);

                // Received message 
                string content = string.Empty;

                if (bytesRead > 0)
                {
                    if (NMensaje == "1001")
                        SignalLinkRegistration(buffer, bytesRead);
                    else if (NMensaje == "0001" && bandera_video == true)
                        LivePreviewRequest();
                    else if (NMensaje == "0001")
                        Heartbeat();
                    else if (NMensaje == "1002")
                        MediaLinkRegistration(buffer, bytesRead);
                    else if (NMensaje == "0011")
                        MediaData(buffer, bytesRead, true);
                    else if (bandera_video == true)
                        MediaData(buffer, bytesRead, false);
                    //Console.WriteLine("no indentificado");

                    // Continues to asynchronously receive data
                    byte[] buffernew = new byte[1024];
                    obj[0] = buffernew;
                    obj[1] = handler;
                    handler.BeginReceive(buffernew, 0, buffernew.Length,
                        SocketFlags.None,
                        new AsyncCallback(ReceiveCallback), obj);
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }
        public void MediaData(byte[] buffer, int bytesRead, bool mensaje_normal)
        {
            try
            {
                if (mensaje_normal == true)
                {
                    Console.WriteLine("MediaData normal");

                    byte[] data = new byte[buffer.Length - 20];

                    Array.Copy(buffer, 20, data, 0, data.Length);

                    //AppendAllBytes(@"C:\xampp\htdocs\video\mov", data);

                    //AppendAllBytes(@"C:\xampp\htdocs\video\mov", buffer);

                    /*Thread thread = new Thread(() => EnviarMensaje(buffer));
                    thread.Start();*/

                    /*Thread thread = new Thread(() => EnviarMensaje(data));
                    thread.Start();*/

                    bandera_video = true;
                }
                else
                {
                    Console.WriteLine("MediaData mal");

                    byte[] data = new byte[buffer.Length];

                    Array.Copy(buffer, 0, data, 0, data.Length);

                    /*Thread thread = new Thread(() => EnviarMensaje(buffer));
                    thread.Start();*/

                    /*Thread thread = new Thread(() => EnviarMensaje(data));
                    thread.Start();*/

                    //AppendAllBytes(@"C:\xampp\htdocs\video\mov", data);
                    //AppendAllBytes(@"C:\xampp\htdocs\video\mov", buffer);
                    //File.WriteAllBytes("M:\\test1\\mov_test", data);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void MediaLinkRegistration(byte[] buffer, int bytesRead)
        {
            try
            {
                Console.WriteLine("MediaLinkRegistration");
                JObject data = JObject.Parse(Encoding.Default.GetString(buffer, 8, bytesRead));
                //'{"ss":"12FB-01DE-0001-0203","err": "0"}';
                JObject data_send = new JObject(
                        new JProperty("ss", (string)data["ss"]),
                        new JProperty("err", "0")
                    );

                SendMessage("4002", data_send.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SignalLinkRegistration(byte[] buffer, int bytesRead)
        {
            try
            {
                Console.WriteLine("SignalLinkRegistration");

                JObject data = JObject.Parse(Encoding.Default.GetString(buffer, 8, bytesRead));

                //'{"ss":"12FB-01DE-0001-0203","err": "0"}';
                JObject data_send = new JObject(
                        new JProperty("ss", (string)data["ss"]),
                        new JProperty("err", "0")
                    );

                SendMessage("4001", data_send.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Heartbeat()
        {
            try
            {
                Console.WriteLine("Heartbeat");
                SendMessage("0001", string.Empty);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void LivePreviewRequest()
        {
            try
            {
                Console.WriteLine("LivePreviewRequest");

                JObject data_send = new JObject(
                    new JProperty("ss", "12FB-01DE-0001-0203-video"),
                    new JProperty("si", "1"),
                      //   new JProperty("srv", "172.16.12.10:6608"),
                      new JProperty("srv", "200.52.220.238:5000"),
                    new JProperty("ch", "1")
                );

                SendMessage("4010", data_send.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SendMessage(string NumMessage, string Message)
        {
            try
            {
                // Prepare the reply message 
                byte[] byteData =
                        Encoding.Default.GetBytes(Message);


                byte[] HeaderID = Encoding.Default.GetBytes("H");
                byte[] HeaderVersion = new byte[] { 1 };
                byte[] HeaderTypeMessage = StringToByteArray(NumMessage);
                byte[] HeaderLength = BitConverter.GetBytes(byteData.Length);


                byte[] dataSend = HeaderID.Concat(HeaderVersion).Concat(HeaderTypeMessage)
                                    .Concat(HeaderLength).Concat(byteData).ToArray();

                // Sends data asynchronously to a connected Socket 
                handler.BeginSend(dataSend, 0, dataSend.Length, 0,
                    new AsyncCallback(SendCallback), handler);

                // Send_Button.IsEnabled = false;
                // Close_Button.IsEnabled = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                // A Socket which has sent the data to remote host 
                Socket handler = (Socket)ar.AsyncState;

                // The number of bytes sent to the Socket 
                int bytesSend = handler.EndSend(ar);
                Console.WriteLine(
                    "Sent {0} bytes to Client", bytesSend);
            }
            catch (Exception exc) { /*MessageBox.Show(exc.ToString());*/ }
        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            Array.Reverse(bytes);
            return bytes;
        }
        public usuarios get(string id)
        {
            try
            {
                duesEntities db = new duesEntities();
                usuarios Usuario = db.usuarios.Find(long.Parse(id));

                return Usuario;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<String> Login(string usuario, string password)
        {
            List<string> tokens = new List<string>();
            try
            {
                duesEntities db = new duesEntities();
                usuarios Usuario = db.usuarios.Where(u => u.Usuario == usuario && u.Password == password).SingleOrDefault();

                if (Usuario == null)
                      throw new Exception("Usuario o contraseña invalidos");

                tokens Token = new tokens();
                Token.Token = GenerarToken(Usuario.Id.ToString(),Usuario.Usuario,Usuario.Password);
                Token.FechaUltimaModificacion = DateTime.Now;

                db.tokens.Add(Token);
                db.SaveChanges();

                
                tokens.Add(Token.Token);                
                return tokens;
            }
            catch (Exception ex)
            {
                Error(ex, "El usuario");
                tokens.Add("");
                return tokens;
            }
        }
        public static string GenerarToken(string id, string user, string password)
        {
            try
            {
                var payload = new Dictionary<string, object>()
                {
                    { "id", id },
                    { "user", user },
                    { "password", password },
                    { "Fecha",DateTime.Now}
                };
                var secretKey = "dues2017";
                return JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public String getId(string usuario, string password)
        {
            List<string> tokens = new List<string>();
            try
            {
                duesEntities db = new duesEntities();
                usuarios Usuario = db.usuarios.Where(u => u.Usuario == usuario && u.Password == password).SingleOrDefault();

                if (Usuario == null)
                    throw new Exception("Usuario o contraseña invalidos");

                String id = Usuario.Id.ToString();
                
                return id;
            }
            catch (Exception ex)
            {
                Error(ex, "El usuario");
                tokens.Add("");
                return "";
            }
        }

        public String getTipo(string usuario, string password)
        {
            List<string> tokens = new List<string>();
            try
            {
                duesEntities db = new duesEntities();
                usuarios Usuario = db.usuarios.Where(u => u.Usuario == usuario && u.Password == password).SingleOrDefault();

                if (Usuario == null)
                    throw new Exception("Usuario o contraseña invalidos");

                String tipo = Usuario.Tipo.ToString();

                return tipo;
            }
            catch (Exception ex)
            {
                Error(ex, "El usuario");
                tokens.Add("");
                return "";
            }
        }
    }
}
