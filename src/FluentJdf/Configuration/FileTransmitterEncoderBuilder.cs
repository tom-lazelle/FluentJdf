﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Core.CodeContracts;
using FluentJdf.Transmission;
using FluentJdf.LinqToJdf;

namespace FluentJdf.Configuration {

    /// <summary>
    /// Settings for FileTransmitterEncoder.
    /// </summary>
    public class FileTransmitterEncoderBuilder : SettingsBuilderBase {
        EncodingSettings encodingSettings;
        FileTransmitterEncoder encoder;
        FluentJdfLibrary fluentJdfLibrary;

        internal FileTransmitterEncoderBuilder(FluentJdfLibrary fluentJdfLibrary, EncodingSettings encodingSettings, FileTransmitterEncoder encoder)
            : base(fluentJdfLibrary) {
            ParameterCheck.ParameterRequired(encodingSettings, "encodingSettings");
            this.fluentJdfLibrary = fluentJdfLibrary;
            this.encodingSettings = encodingSettings;
            this.encoder = encoder;
        }

        /// <summary>
        /// Add a new FileTransmitterEncoder
        /// </summary>
        /// <param name="id">The id of the encoder</param>
        /// <param name="urlBase">The url base</param>
        /// <param name="useMime">UseMime</param>
        /// <param name="nameValues">Extra Values</param>
        /// <returns></returns>
        public FileTransmitterEncoderBuilder FileTransmitterEncoder(string id, string urlBase, bool useMime = false, IDictionary<string, string> nameValues = null) {
            ParameterCheck.StringRequiredAndNotWhitespace(id, "id");
            ParameterCheck.StringRequiredAndNotWhitespace(urlBase, "urlBase");

            if (encodingSettings.FileTransmitterEncoders.ContainsKey(id)) {
                throw new JdfException(string.Format("FileTransmitterEncoder Id already exists {0}", id));
            }
            var newEncoder = new FileTransmitterEncoder(id, urlBase, useMime, nameValues);
            encodingSettings.AddFileTransmitterEncoders(newEncoder);
            return new FileTransmitterEncoderBuilder(fluentJdfLibrary, encodingSettings, newEncoder);
        }

        /// <summary>
        /// Add a new FileTransmitterEncoder
        /// </summary>
        /// <param name="id">The id of the encoder</param>
        /// <param name="urlBase">The url base</param>
        /// <param name="useMime">UseMime</param>
        /// <param name="nameValues">Extra Values</param>
        /// <returns></returns>
        public FileTransmitterEncoderBuilder FileTransmitterEncoder(string id, Uri urlBase, bool useMime = false, IDictionary<string, string> nameValues = null) {
            ParameterCheck.StringRequiredAndNotWhitespace(id, "id");
            ParameterCheck.ParameterRequired(urlBase, "urlBase");

            return this.FileTransmitterEncoder(id, urlBase.ToString(), useMime, nameValues);
        }

        /// <summary>
        /// Add Folder Info
        /// </summary>
        /// <param name="type">The <see cref="FolderInfoTypeEnum"/> type</param>
        /// <param name="destinationFolder">The destination folder</param>
        /// <param name="referenceFolder">The reference folder</param>
        /// <param name="order">The optional order to write the files.</param>
        /// <param name="nameValues">Additional Parameters</param>
        /// <returns></returns>
        public FileTransmitterEncoderBuilder FolderInfo(FolderInfoTypeEnum type, string destinationFolder,
                                                        string referenceFolder = null, int order = 0, IDictionary<string, string> nameValues = null) {
            return this.FolderInfo(type, destinationFolder, false, referenceFolder, order, nameValues);
        }

        /// <summary>
        /// Add Folder Info
        /// </summary>
        /// <param name="type">The <see cref="FolderInfoTypeEnum"/> type</param>
        /// <param name="destinationFolder">The destination folder</param>
        /// <param name="referenceFolder">The reference folder</param>
        /// <param name="order">The optional order to write the files.</param>
        /// <param name="nameValues">Additional Parameters</param>
        /// <returns></returns>
        public FileTransmitterEncoderBuilder FolderInfo(FolderInfoTypeEnum type, Uri destinationFolder,
                                                        Uri referenceFolder = null, int order = 0, IDictionary<string, string> nameValues = null) {
            return this.FolderInfo(type, destinationFolder.ToString(), false, referenceFolder != null ? referenceFolder.ToString() : null, order, nameValues);
        }

        /// <summary>
        /// Add Folder Info
        /// </summary>
        /// <param name="type">The <see cref="FolderInfoTypeEnum"/> type</param>
        /// <param name="destinationFolder">The destination folder</param>
        /// <param name="referenceFolder">The reference folder</param>
        /// <param name="order">The optional order to write the files.</param>
        /// <param name="suppress">Suppress the output</param>
        /// <param name="nameValues">Additional Parameters</param>
        /// <returns></returns>
        public FileTransmitterEncoderBuilder FolderInfo(FolderInfoTypeEnum type, string destinationFolder,  bool suppress,
                                                        string referenceFolder = null, int order = 0, IDictionary<string, string> nameValues = null) {
            encoder.AddFolderInfo(new FileTransmitterFolderInfo(type, destinationFolder, referenceFolder ?? destinationFolder, order, suppress, nameValues));
            return this;
        }

        /// <summary>
        /// Add Folder Info
        /// </summary>
        /// <param name="type">The <see cref="FolderInfoTypeEnum"/> type</param>
        /// <param name="destinationFolder">The destination folder</param>
        /// <param name="referenceFolder">The reference folder</param>
        /// <param name="order">The optional order to write the files.</param>
        /// <param name="suppress">Suppress the output</param>
        /// <param name="nameValues">Additional Parameters</param>
        /// <returns></returns>
        public FileTransmitterEncoderBuilder FolderInfo(FolderInfoTypeEnum type, Uri destinationFolder, bool suppress,
                                                        Uri referenceFolder = null, int order = 0, IDictionary<string, string> nameValues = null) {
            return this.FolderInfo(type, destinationFolder.ToString(), suppress, referenceFolder != null ? referenceFolder.ToString() : null, order, nameValues);
        }

    }
}
