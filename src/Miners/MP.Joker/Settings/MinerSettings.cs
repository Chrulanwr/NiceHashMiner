﻿using Newtonsoft.Json;
using NHM.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MP.Joker.Settings
{
    internal class MinerSettings
    {
        public const string USERNAME_TEMPLATE = "{USERNAME}";
        public const string API_PORT_TEMPLATE = "{API_PORT}";
        public const string POOL_URL_TEMPLATE = "{POOL_URL}";
        public const string POOL_PORT_TEMPLATE = "{POOL_PORT}";
        public const string ALGORITHM_TEMPLATE = "{ALGORITHM}";
        public const string DEVICES_TEMPLATE = "{DEVICES}";
        public const string EXTRA_LAUNCH_PARAMETERS_TEMPLATE = "{EXTRA_LAUNCH_PARAMETERS}";

        [JsonProperty("conection_type")]
        public NhmConectionType NhmConectionType { get; set; } = NhmConectionType.NONE;

        [JsonProperty("devices_separator")]
        public string DevicesSeparator { get; set; } = ",";

        [JsonProperty("default_command_line")]
        public string DefaultCommandLine { get; set; } = $"--user {USERNAME_TEMPLATE} --pool {POOL_URL_TEMPLATE}:{POOL_PORT_TEMPLATE} --algo {ALGORITHM_TEMPLATE} --apiport {API_PORT_TEMPLATE} --devices {DEVICES_TEMPLATE} {EXTRA_LAUNCH_PARAMETERS_TEMPLATE}";
    }
}
