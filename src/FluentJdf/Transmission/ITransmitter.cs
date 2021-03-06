﻿using System;
using FluentJdf.Encoding;
using FluentJdf.Messaging;

namespace FluentJdf.Transmission {
    /// <summary>
    /// Interface for transmitting data.
    /// </summary>
    public interface ITransmitter {
        /// <summary>
        /// Transmit data to the given URI.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="partsToSend"></param>
        /// <returns>A result that includes parsed JMF results if available.</returns>
        IJmfResult Transmit(Uri uri, ITransmissionPartCollection partsToSend);

        /// <summary>
        /// Transmit data to the given uri (string).
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="partsToSend"></param>
        /// <returns>A result that includes parsed JMF results if available.</returns>
        IJmfResult Transmit(string uri, ITransmissionPartCollection partsToSend);
    }
}