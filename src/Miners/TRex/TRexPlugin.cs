﻿using NHM.MinerPluginToolkitV1;
using NHM.MinerPluginToolkitV1.Configs;
using NHM.Common.Algorithm;
using NHM.Common.Device;
using NHM.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TRex
{
    public partial class TRexPlugin : PluginBase
    {
        public TRexPlugin()
        {
            // mandatory init
            InitInsideConstuctorPluginSupportedAlgorithmsSettings();
            // set default internal settings
            MinerOptionsPackage = PluginInternalSettings.MinerOptionsPackage;
            GetApiMaxTimeoutConfig = PluginInternalSettings.GetApiMaxTimeoutConfig;
            DefaultTimeout = PluginInternalSettings.DefaultTimeout;
            MinerBenchmarkTimeSettings = PluginInternalSettings.BenchmarkTimeSettings;
            // https://github.com/trexminer/T-Rex/releases 
            MinersBinsUrlsSettings = new MinersBinsUrlsSettings
            {
                BinVersion = "0.17.2",
                ExePath = new List<string> { "t-rex.exe" },
                Urls = new List<string>
                {
                    "https://github.com/trexminer/T-Rex/releases/download/0.17.2/t-rex-0.17.2-win-cuda11.0.zip", // original
                }
            };
            PluginMetaInfo = new PluginMetaInfo
            {
                PluginDescription = "T-Rex is a versatile cryptocurrency mining software for NVIDIA devices.",
                SupportedDevicesAlgorithms = SupportedDevicesAlgorithmsDict()
            };
        }

        public override string PluginUUID => "03f80500-94ec-11ea-a64d-17be303ea466";

        public override Version Version => new Version(14, 0);

        public override string Name => "TRex";

        public override string Author => "info@nicehash.com";

        public override Dictionary<BaseDevice, IReadOnlyList<Algorithm>> GetSupportedAlgorithms(IEnumerable<BaseDevice> devices)
        {
#warning TEMP disable NVIDIA driver check
            ////var isDriverSupported = Checkers.IsCudaCompatibleDriver(Checkers.CudaVersion.CUDA_11_0_3_Update1, CUDADevice.INSTALLED_NVIDIA_DRIVERS); // TODO restore after major version 15
            //var CUDA11 = new Version(451, 82);
            //var isDriverSupported = CUDADevice.INSTALLED_NVIDIA_DRIVERS >= CUDA11;  // TODO <= CUDA 11 is not inside the toolkit before miner plugins major version 15
            var isDriverSupported = true;  // always enable workaround
            var cudaGpus = devices.Where(dev => dev is CUDADevice cuda && cuda.SM_major >= 3 && isDriverSupported).Cast<CUDADevice>();
            var supported = new Dictionary<BaseDevice, IReadOnlyList<Algorithm>>();

            foreach (var gpu in cudaGpus)
            {
                var algos = GetSupportedAlgorithmsForDevice(gpu);
                if (algos.Count > 0) supported.Add(gpu, algos);
            }

            return supported;
        }

        protected override MinerBase CreateMinerBase()
        {
            return new TRex(PluginUUID);
        }        

        public override IEnumerable<string> CheckBinaryPackageMissingFiles()
        {
            var pluginRootBinsPath = GetBinAndCwdPaths().Item2;
            return BinaryPackageMissingFilesCheckerHelpers.ReturnMissingFiles(pluginRootBinsPath, new List<string> { "t-rex.exe" });
        }

        public override bool ShouldReBenchmarkAlgorithmOnDevice(BaseDevice device, Version benchmarkedPluginVersion, params AlgorithmType[] ids)
        {
            try
            {}
            catch(Exception ex)
            {}
            return false;
        }
    }
}
