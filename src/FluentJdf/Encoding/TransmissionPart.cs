﻿using System;
using System.IO;
using FluentJdf.Resources;
using FluentJdf.Utility;
using Infrastructure.Core;
using Infrastructure.Core.CodeContracts;
using Infrastructure.Core.Helpers;

namespace FluentJdf.Encoding {
    /// <summary>
    /// A basic transmission part.
    /// </summary>
    public class TransmissionPart : ITransmissionPart {
        Stream stream;

        /// <summary>
        /// This constructor should only be used by the factory.  It
        /// should not be called directly by user code.
        /// </summary>
        public TransmissionPart() {
            
        }

        /// <summary>
        /// Creates a transmission part from the given file name.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException">If the named file does not exist.</exception>
        public TransmissionPart(string fileName, string id = null) {
            ParameterCheck.StringRequiredAndNotWhitespace(fileName, "fileName");

            if (!File.Exists(fileName)) {
                throw new ArgumentException(string.Format(Messages.TransmissionPart_CannotCreatePartAsFileDoesNotExist, fileName));
            }

            InitalizeProperties(fileName, fileName.MimeType(), id);
            InitializePartStream(File.OpenRead(fileName));
        }

        /// <summary>
        /// Creates a transmission part from the given stream. The source
        /// stream is disposed by the constructor.
        /// </summary>
        public TransmissionPart(Stream sourceStream, string name, string mimeType, string id = null) {
            ParameterCheck.ParameterRequired(sourceStream, "sourceStream");
            ParameterCheck.StringRequiredAndNotWhitespace(name, "name");
            ParameterCheck.StringRequiredAndNotWhitespace(mimeType, "mimeType");

            InitalizeProperties(name, mimeType, id);
            InitializePartStream(sourceStream);
        }

        void InitalizeProperties(string name, string mimeType, string id) {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = string.Format("P_{0}", UniqueGenerator.MakeUnique());
            }

            Name = name;
            MimeType = mimeType;
            Id = id;
        }

        void InitializePartStream(Stream sourceStream) {
            if (sourceStream.CanSeek)
            {
                stream = sourceStream;
            }
            else {
                stream = new TempFileStream();
                using (sourceStream) {
                    sourceStream.CopyTo(stream);
                }
            }
            stream.Seek(0, SeekOrigin.Begin);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of the transmission part.
        /// </summary>
        /// <param name="isDisposing"></param>
        protected virtual void Dispose(bool isDisposing) {
            if (isDisposing) {
                if (stream != null) {
                    stream.Dispose();
                    stream = null;
                }
            }
        }

        /// <summary>
        /// Gets the stream associated with the part.
        /// </summary>
        public virtual Stream CopyOfStream() {
            var tempStream = new TempFileStream();
            stream.CopyTo(tempStream);
            tempStream.Seek(0, SeekOrigin.Begin);
            stream.Seek(0, SeekOrigin.Begin);

            return tempStream;
        }

        /// <summary>
        /// Gets the name of the part.  May not be unique.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the id of the part.  Must be unique
        /// within the context of all parts in a single
        /// collection.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the mime type of the part.
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// Initialize an instance from the factory.
        /// </summary>
        public void Initialize(string name, Stream stream, string mimeType, string id = null) {
            ParameterCheck.StringRequiredAndNotWhitespace(name, "name");
            ParameterCheck.ParameterRequired(stream, "stream");
            ParameterCheck.StringRequiredAndNotWhitespace(mimeType, "mimeType");

            InitializePartStream(stream);
            InitalizeProperties(name, mimeType, id);
        }
    }
}