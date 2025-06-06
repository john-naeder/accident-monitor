﻿using System.Security.Cryptography.X509Certificates;

namespace iot_monitor.Infrastructure.MQTT
{
    public class MqttConnectionConfiguration
    {
        public string? ClientID { get; set; } = "AccidentMonitor";
        public string Broker { get; set; } = "localhost";
        public string Protocol { get; set; } = "mqtt";
        public int Port { get; set; } = 1883;
        public bool UseTls { get; set; } = false;
        public bool UseWebSocket { get; set; } = false;
        public int ProtocolVersion { get; set; } = 5;
        public X509Certificate2Collection? TrustChain { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? CertPath { get; set; }
        public string[] SubTopics { get; set; } = [];
        public int QoS { get; set; } = 0;
        public bool CleanSession { get; set; } = true;
        public bool AllowUntrustedCertificates { get; set; } = false;
        public bool IgnoreCertificateRevocationErrors { get; set; } = false;
        public bool IgnoreCertificateChainErrors { get; set; } = false;
        public int KeepAlivePeriod { get; set; } = 60;
    }
}
