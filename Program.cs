// Copyright (c) Microsoft. All rights reserved.
using System.Security.Authentication;
using MQTTnet.Client;
using MQTTnet.Formatter;
using MQTTnet;

namespace SampleDotnetMqtt
{


    class Program
    {


        public static int Main() => MainAsync().Result;

        static async Task<int> MainAsync()
        {
            Console.WriteLine("Started MQTT client.");
            var mqttFactory = new MqttFactory();
            var satToken = File.ReadAllText("/var/run/secrets/tokens/mqtt-client-token");
            Console.WriteLine("SAT token read.");
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                // To connect to broker
                var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("azedge-dmqtt-frontend", 1883)
                .WithProtocolVersion(MqttProtocolVersion.V500)
                .WithClientId("sampleid")
                .WithCredentials("$sat", satToken )
                .Build();

                var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                Console.WriteLine("The MQTT client is connected.");
                // To publish messages
                var counter = 1;
                while (true) {
                    var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic("sampletopic")
                    .WithPayload("samplepayload" + counter)
                    .Build();

                    await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
                    Console.WriteLine("The MQTT client published a message.");
                }


            }
            return 0;
        }

    }
}