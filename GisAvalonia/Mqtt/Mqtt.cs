using MQTTnet.Server;
using MQTTnet;
using System;
using MQTTnet.Client;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace GisAvalonia.Mqtt
{
    internal class Mqtt
    {
        static bool isSucess=false;
        public static async void MqttServer()
        {
            if(isSucess) return;
            var optionsBuilder = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithDefaultEndpointPort(1083)
            .WithConnectionBacklog(10)
            .WithClientCertificate(ClientValite)
            .Build();
            var _mqttServer = new MqttFactory().CreateMqttServer(optionsBuilder);
            _mqttServer.StartedAsync += OnMqttServerStart;
            _mqttServer.StoppedAsync += OnMqttServerStopped;

            //客户端接入
            _mqttServer.ClientConnectedAsync += _mqttServer_ClientConnectedAsync;

            //客户端断开
            _mqttServer.ClientDisconnectedAsync += _mqttServer_ClientDisconnectedAsync;

            //主题消息
            _mqttServer.ClientSubscribedTopicAsync += MqttServer_ClientSubscribedTopicAsync;

            //主题订阅需要实现IMqttServerClientSubscribedTopicHandler
            _mqttServer.ClientUnsubscribedTopicAsync += _mqttServer_ClientUnsubscribedTopicAsync;

            _mqttServer.ApplicationMessageEnqueuedOrDroppedAsync += _mqttServer_ApplicationMessageEnqueuedOrDroppedAsync;
            //启动监听需要实现IMqttServerStartedHandler
           // _mqttServer.StartedHandler = new ServerStarteOrStopped();
            //停止监听 IMqttServerStoppedHandler
           // _mqttServer.StoppedHandler = new ServerStarteOrStopped();
            //服务启动
            await _mqttServer.StartAsync();
            isSucess = true;
        }

        private static bool ClientValite(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static async Task _mqttServer_ApplicationMessageEnqueuedOrDroppedAsync(ApplicationMessageEnqueuedEventArgs arg)
        {
           // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "MQ启动");
        }

        private static async Task _mqttServer_ClientUnsubscribedTopicAsync(ClientUnsubscribedTopicEventArgs arg)
        {
           // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "UnsubscribedTopic");
        }

        private static async Task MqttServer_ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs arg)
        {
            //await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "ClientSubscribedTopic");
        }

        private static async Task _mqttServer_ClientDisconnectedAsync(ClientDisconnectedEventArgs arg)
        {
            //await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "ClientDisconnected");
        }

        private static async Task _mqttServer_ClientConnectedAsync(ClientConnectedEventArgs arg)
        {
           // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "ClientConnected");
        }

        private static async Task OnMqttServerStopped(EventArgs args)
        {
           // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "MqttServerStopped");
        }

        private static async Task OnMqttServerStart(EventArgs args)
        {
            //await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "MqttServerStart");
        }

        public static  IMqttClient Client()
        {
            var options = new MqttClientOptions();

            options.ClientId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            //设置服务器地址与端口
            options.ChannelOptions = new MqttClientTcpOptions()
            {

                Server = "127.0.0.1",
                Port = 1083,
                 
            };
            ////设置账号与密码
            //options.Credentials = new MqttClientCredentials("admin")
            //{
               
            //};
            options.CleanSession = true;

            //保持期
            options.KeepAlivePeriod = TimeSpan.FromSeconds(100.5);

            List<MqttUserProperty> lstUsers = new List<MqttUserProperty>();
            lstUsers.Add(new MqttUserProperty("admin", "123"));

            //options.UserProperties = lstUsers;
            //构建客户端对象
            var _mqttClient = new MqttFactory().CreateMqttClient();

            try
            {
                //绑定消息接收方法
                _mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync;

                //绑定连接成功状态接收方法
                _mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync;

                //绑定连接断开状态接收方法
                _mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync;

                //启动连接
              var ret=  _mqttClient.ConnectAsync(options).Result;
              
                return _mqttClient;
                
            }
            catch
            {
                Console.WriteLine($"连接失败");
            }
            return null;
        }

        private static async Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
           // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "Client_Disconnected");
        }

        private static async Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "_mqttClient_ConnectedAsync");
        }

        private static async Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
           // await Log.GisLog.Logger.LogAsync(Log.LogLevel.Info, "_mqttClient_ApplicationMessageReceivedAsync");
        }

        public static void PublishStringAsync(IMqttClient client, string topic, string message)
        {
            if (client != null)
            {

                client.PublishStringAsync(topic, message);
            }
        }
        public static void SubscribeAsync(IMqttClient client, string topic,Action<string,string> func=null)
        {
            if (client != null)
            {
                List<MqttTopicFilter> lst = new List<MqttTopicFilter>();
                lst.Add(new MqttTopicFilter() //订阅消息对象
                {
                    Topic = topic,  //订阅消息主题
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce  //消息类型
                });
                List<MqttUserProperty> lstUsers = new List<MqttUserProperty>();
                lstUsers.Add(new MqttUserProperty("admin", "123"));
                var optt = new MqttClientSubscribeOptions() { TopicFilters = lst };

              var ret=  client.SubscribeAsync(optt);

                client.ApplicationMessageReceivedAsync += async (p) =>
                {
                    if (func != null)
                    {
                       await  Task.Run(() =>
                        {
                            string topic = p.ApplicationMessage.Topic;
                            string msg = UTF8Encoding.UTF8.GetString(p.ApplicationMessage.PayloadSegment.Array);
                            func(topic, msg);
                        });

                    }
                };
            }
        }
    }
}
