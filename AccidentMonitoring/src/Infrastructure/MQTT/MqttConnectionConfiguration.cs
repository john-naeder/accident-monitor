﻿using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using MQTTnet.Formatter;

namespace AccidentMonitoring.Infrastructure.MQTT
{
    public class MqttConnectionConfiguration
    {
        public string? ClientID { get; set; } = "AccidentMonitoring";
        public string Broker { get; set; } = "localhost";
        public string Protocol { get; set; } = "mqtt";
        public int Port { get; set; } = 1883;
        public bool UseTls { get; set; } = false;
        public int ProtocolVersion { get; set; } = 5;
        public X509Certificate2Collection? TrustChain { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? CertPath { get; set; }
        public string[] SubTopics { get; set; } = [];
        public int QoS { get; set; } = 0;
    }
}
