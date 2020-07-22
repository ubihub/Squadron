using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using System.Linq;
using System.Globalization;
using UnityEngine.Networking;
using System.Collections;
//using ThreadPriority = System.Threading.ThreadPriority;

namespace Simple.Trans.Hololens
{


    public class TextReceiver : MonoBehaviour
    {
        #region private members 
        /// <summary> 	
        /// TCPListener to listen for incomming TCP connection 	
        /// requests. 	
        /// </summary> 	
        private TcpListener tcpListener;
        /// <summary> 
        /// Background thread for TcpServer workload. 	
        /// </summary> 	
        private Thread tcpListenerThread;
        /// <summary> 	
        /// Create handle to connected tcp client. 	
        /// </summary> 	
        private TcpClient connectedTcpClient;
        #endregion
        public string Default;
        public Material selfMat, worldMat;
        public GameObject staticGameObject;
        public GameObject Content;
        public GameObject Fingerprint;
        public GameObject textField;
        public GameObject starCraft;
        public GameObject MRCamera;
        public GameObject Arrow;
        public GameObject Duo;
        private static float previousX = 0.0f;
        private static float previousY = 0.8050001f;
        private static float previousZ = -0.9590001f;
        TextMesh t = new TextMesh();
        Text text;
        InputField input;
        static int index = 0;
        static int count = 0;
        string inputMessage ="";
        NetworkStream stream;
        int length;
        Byte[] bytes = new byte[2048];
        String serverIP = "192.168.43.1";
        String serverPort = "9999";
        private TcpClient socketConnection;
        private readonly object token = new object();
        private Boolean flag = false;
        public float missileSpeed = 200.0f;
        public float bulletSpeed = 300.0f;
        public Rigidbody missile;
        public Rigidbody bullet;
        Rigidbody missileClone;
        Rigidbody bulletClone;
        String chatMessages = "";
        static String prevMessage = "";

        Thread ChildThread = null;
        Queue myQ = new Queue();

        private Quaternion targetRotation;
        private Quaternion pendingTargetRotation;
        private Quaternion lastRotation;
        private float speed;
        private String speedStr = "";

        ////EventWaitHandle ChildThreadWait = new EventWaitHandle(true, EventResetMode.ManualReset);
        //EventWaitHandle MainThreadWait = new EventWaitHandle(true, EventResetMode.ManualReset);


        //private static Mutex mutex = new Mutex();
        //private static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);
        //private static EventWaitHandle clearCount = new EventWaitHandle(false, EventResetMode.AutoReset);
        //       NetworkStream stream;
        void Start()
        {
            try
            {
                //            Debug.Log("TCPTestServer : Start");

                serverIP = GetDefaultGateway();
                socketConnection = new TcpClient(serverIP, 5555);
                if (socketConnection == null) return;
                //            Debug.Log("TCPTestServer : Start");
                // Get a stream object for writing. 			

                //tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
                //tcpListenerThread.IsBackground = false;
                //tcpListenerThread.Priority = ThreadPriority.Highest;
                //tcpListenerThread.Start();

                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    string clientMessage = "This is a message from HoloLens.";
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    //Debug.Log("Client sent his message - should be received by server");
                }
                socketConnection.Close();
                inputMessage = "";

                if (textField != null) input = textField.transform.GetComponent<InputField>();
                input.onEndEdit.AddListener(delegate { SendInput(input); });

                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[3];
                //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

                tcpListener = new TcpListener(ipAddress, 8050);

                tcpListener.Start();

            }
            catch (Exception exception)
            {
                Debug.Log("Exception: " + exception);
            }

        }
/*
        void ChildThreadLoop()
        {
            ChildThreadWait.Reset();
            ChildThreadWait.WaitOne();

            while (true)
            {
                ChildThreadWait.Reset();
                WaitHandle.SignalAndWait(MainThreadWait, ChildThreadWait);
            }
        }

*/
        void Awake()
        {
            //MissileFire();
        }


        private void SendInput(InputField mainInputField)
        {
            if (input.text.Length > 0)
            {
                serverIP = GetDefaultGateway();
                socketConnection = new TcpClient(serverIP, 5555);
                if (socketConnection == null) return;
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    string clientMessage = mainInputField.text;
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    //                   Debug.Log("Client sent his message - should be received by server");
                }
                socketConnection.Close();
            }
            else if (input.text.Length == 0)
            {
                Debug.Log("Main Input Empty");
            }
        }

        public string GetDefaultGateway()
        {
            var defaultGateway =
            from nics in NetworkInterface.GetAllNetworkInterfaces()
            from props in nics.GetIPProperties().GatewayAddresses
            where nics.OperationalStatus == OperationalStatus.Up
            select props.Address.ToString();

            return defaultGateway.First();
        }

        void MissileFire()
        {
            if (index < 2)
            {
                //if (bulletClone != null && bullet != null) bulletClone.velocity = bullet.transform.forward * bulletSpeed;
                index++;
                return;
            }
            else index = 0;
            Vector3 newPosition = new Vector3(starCraft.transform.position.x, starCraft.transform.position.y, starCraft.transform.position.z + 5);
            Vector3 rot = starCraft.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x * 2, rot.y * 2, rot.z);
            Quaternion bulletRot = Quaternion.Euler(rot);
            missile.transform.rotation = starCraft.transform.rotation;
            missileClone = (Rigidbody)Instantiate(missile, newPosition, bulletRot);
            missileClone.velocity = missile.transform.forward * missileSpeed;
            //Debug.Log("missileClone.velocity " );

        }


        void Gun()
        {

            Vector3 newPosition = new Vector3(starCraft.transform.position.x, starCraft.transform.position.y, starCraft.transform.position.z + 5);
            Vector3 rot = starCraft.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x * 2, rot.y * 2, rot.z);
            Quaternion bulletRot = Quaternion.Euler(rot);
            bullet.transform.rotation = starCraft.transform.rotation;
            bulletClone = (Rigidbody)Instantiate(bullet, newPosition, bulletRot);
            bulletClone.velocity = bullet.transform.forward * bulletSpeed;

        }

        void Update()
        {
            ListenForIncommingRequests();
            if (flag)
            {
                flag = false;
                return;
            }
            else
            {
                flag = true;
            }

            //if (bulletClone != null && bullet != null) bulletClone.velocity = bullet.transform.forward * bulletSpeed;

            //MainThreadWait.WaitOne();
            //MainThreadWait.Reset();
            //ChildThreadWait.Set();
            //Thread.Yield();
            //Thread.Sleep(100);
            //Thread.SpinWait(10);
            //float angleX = Mathf.MoveTowardsAngle(transform.eulerAngles.x, 1.0f, 1.0f * Time.deltaTime);
            //float angleY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, 1.0f, 1.0f * Time.deltaTime);
            //float angleZ = Mathf.MoveTowardsAngle(transform.eulerAngles.z, 1.0f, 1.0f * Time.deltaTime);
            //if (Duo != null) Duo.transform.eulerAngles = new Vector3(angleX, angleY, angleZ);
            //if (Duo != null) Duo.transform.position = new Vector3(Duo.transform.position.x * Time.deltaTime, Duo.transform.position.y, Duo.transform.position.z);
            //if (Duo != null) Duo.transform.position = Duo.transform.position + Duo.transform.forward * Time.deltaTime * 50.0f;
            //if (Duo != null) Duo.transform.Translate(Vector3.forward * Time.deltaTime);
            //starCraft.transform.position += starCraft.transform.forward * Time.deltaTime * 50.0f;
            //float distance = Vector3.Distance(MRCamera.transform.position, starCraft.transform.position);
            //float distance =  50;
            //MRCamera.transform.position = (MRCamera.transform.position - starCraft.transform.position).normalized * distance + starCraft.transform.position;

            {
                var incommingData = new byte[length];
                if (incommingData.Length > 0)
                {
                    Array.Copy(bytes, 0, incommingData, 0, length);
                    inputMessage = Encoding.ASCII.GetString(incommingData);
                    //Debug.Log("TextReceiver : Update : inputMessage : " + inputMessage);
                    //                Debug.Log("client message received as: " + inputMessage);
                }
            }


            if (inputMessage != null && inputMessage.Contains("^") || inputMessage.Contains("}") || inputMessage.Contains("]"))
            {
                //Debug.Log("TextReceiver : Update : inputMessage : " + inputMessage);

                if (inputMessage.Contains("}")) StartCoroutine("MissileFire");
                if (inputMessage.Contains("]")) StartCoroutine("Gun");
                string x = "", y = "", z = "";
                char[] delimeter = { '^', '}', ']', 'x', 'y', 'z' };
                string[] xyz = inputMessage.Split(delimeter);
                if (xyz.Length > 1) speedStr = xyz[1];
                if (xyz.Length > 2) x = xyz[2];
                if (xyz.Length > 3) y = xyz[3];
                if (xyz.Length > 4) z = xyz[4];
                 if (x.Length > 7) x = x.Substring(0, 7);
                if (y.Length > 7) y = y.Substring(0, 7);
                if (z.Length > 7) z = z.Substring(0, 7);
                //Debug.Log("Update : Speed " + speed + " X " + x + " Y : " + y + " Z: " + z);
                speed = float.Parse(speedStr);
                float xAngle = float.Parse(x);
                float yAngle = float.Parse(y);
                float zAngle = float.Parse(z);
                float adjustX = xAngle - previousX;
                float adjustY = yAngle - previousY;
                float adjustZ = zAngle - previousZ;
                var desiredRotQ = Quaternion.Euler((xAngle + 5) *6, adjustZ * Time.deltaTime, yAngle *6);
                starCraft.transform.rotation = Quaternion.Lerp(starCraft.transform.rotation, desiredRotQ, Time.deltaTime * 10.0f);

                previousX = xAngle;
                previousY = yAngle;
                previousZ = zAngle;

                //StartCoroutine(setTargetRotation(starCraft.transform.rotation));

                 var DuodRot = Quaternion.Euler((xAngle + 5) * 6, adjustZ , yAngle * 10);
                Duo.transform.rotation = Quaternion.RotateTowards(Duo.transform.rotation, DuodRot, 20 * Time.deltaTime);

                Vector3 pos = MRCamera.transform.position;
                Vector3 dir = (MRCamera.transform.position - Duo.transform.position).normalized;
                //Debug.DrawLine(pos, pos + dir * 200, Color.red, Mathf.Infinity);
                Vector3 temp = pos + dir * -400 * ( speed * 0.003f + 1);
                Vector3 temp2 = new Vector3(temp.x, temp.y, temp.z);
                Duo.transform.position = Vector3.Lerp(Duo.transform.position, temp2, 5.0f * Time.deltaTime);
                Vector3 arrow_position = Arrow.transform.position;
                RectTransform arrowRectTransform = Arrow.GetComponent<RectTransform>();
                //Debug.Log(arrowRectTransform.localPosition);
                arrowRectTransform.localPosition = new Vector3(arrowRectTransform.localPosition.x, arrowRectTransform.localPosition.y + adjustZ * -9.0f, arrowRectTransform.localPosition.z);
            }
            else if (inputMessage != null && ! inputMessage.Equals(prevMessage))
            {

                count++;
                //Debug.Log("Update : inputMessage " + inputMessage);
                if (staticGameObject != null) t = staticGameObject.transform.GetComponent<TextMesh>();
                if (t != null) t.text = inputMessage;
                chatMessages +=  "Me : "  + inputMessage + "\n";
                if (Content != null) text = Content.GetComponent<Text>();
                if (text != null) text.text = chatMessages;
                prevMessage = inputMessage;
                inputMessage = "";
                if (Fingerprint != null) Fingerprint.SetActive(false);
                StartCoroutine("Respond");
            }

            inputMessage = "";
        }

        private IEnumerator setTargetRotation(Quaternion rotation)
        {
            pendingTargetRotation = rotation;
            yield return new WaitForSeconds(2);
            targetRotation = pendingTargetRotation;
        }


        private void ListenForIncommingRequests()
        {
            try
            {
                //ChildThreadWait.Reset();
                //ChildThreadWait.WaitOne();

//                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//                IPAddress ipAddress = ipHostInfo.AddressList[3];
                //IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

//                tcpListener = new TcpListener(ipAddress, 8050);
                //tcpListener.Start();


                AsyncCallback OnAccept = null;

                OnAccept = delegate (IAsyncResult ar) {
                    TcpClient client = null;
                    try
                    {

                        //ChildThreadWait.Reset();
                       // WaitHandle.SignalAndWait(MainThreadWait, ChildThreadWait);
                        //
                        // Get the socket that will be used to communicate with the connected client.
                        //
                        client = tcpListener.EndAcceptTcpClient(ar);
                        stream = client.GetStream();
                        length = stream.Read(bytes, 0, bytes.Length);
                        //Debug.Log("delegate (IAsyncResult ar) : length = " + length);

                    }
                    catch (Exception)
                    {

                        return;
                    }
                };
                tcpListener.BeginAcceptTcpClient(OnAccept, null);
            }
            catch (SocketException socketException)
            {
                Debug.Log("SocketException " + socketException.ToString());
            }
            catch (Exception en)
            {
                Debug.Log("Exception " + en.ToString());
            }
        }


        public static void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(ar);
            Console.WriteLine("Client connected completed");

        }

        public IEnumerator Respond()
        {
            yield return new WaitForSeconds(3);
            chatMessages += "\n" + "Jackson : How are you today Fckr?" + "\n";
            if (Content != null) text = Content.GetComponent<Text>();
            if (text != null) text.text = chatMessages;
        }
    }
}