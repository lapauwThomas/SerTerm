﻿using System.Collections.ObjectModel;

namespace SerTerm.PortManagement;


internal static class SerialPortConstants
{
    /// <summary>
    /// Collection with regex patterns for VID_PID_combos
    /// </summary>
    public static ReadOnlyCollection<string> USBVIDPIDRegexList { get; } = new ReadOnlyCollection<string>(new[]
    {
        @"VID_([0-9A-F]+)&PID_([0-9A-F]+)",
    });

    public static class ServiceDescriptions
    {
        /// <summary>
        /// List of service descriptions for USB serial ports, this is not unified/standardized
        /// </summary>
        public static ReadOnlyCollection<string> USBSerialDescriptionList { get; } = new ReadOnlyCollection<string>(new[]
        {
            "usbser",
            "FTSER2K",
        });

        /// <summary>
        /// List of service descriptions for Native serial ports, unsure if this is unified/standardized
        /// </summary>
        public static ReadOnlyCollection<string> NativeSerialDescriptionList { get; } = new ReadOnlyCollection<string>(new[]
        {
            "Serial",
        });
    }
}