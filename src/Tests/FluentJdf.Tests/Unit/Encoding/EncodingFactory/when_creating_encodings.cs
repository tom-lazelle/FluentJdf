﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentJdf.Configuration;
using FluentJdf.Encoding;
using Machine.Specifications;

namespace FluentJdf.Tests.Unit.Encoding.EncodingFactory
{
    [Subject(typeof(FluentJdf.Encoding.EncodingFactory))]
    public class when_creating_encodings {
        static FluentJdf.Encoding.EncodingFactory factory;
        static IEncoding defaultEncoding;
        static IEncoding defaultSinglePartEncoding;
        static IEncoding defaultMultiPartEncoding;

        Establish context = () => {
                                FluentJdf.Configuration.FluentJdfLibrary.Settings.ResetToDefaults();
                                factory = new FluentJdf.Encoding.EncodingFactory();
                            };

        Because of = () => {
                         defaultEncoding = factory.GetEncodingForMimeType("xxx");
                         defaultSinglePartEncoding = factory.GetDefaultEncodingForSinglePart();
                         defaultMultiPartEncoding = factory.GetDefaultEncodingForMultiPart();
                     };

        It should_have_default_encoding_passthrough = () => defaultEncoding.ShouldBeOfType(typeof(FluentJdf.Encoding.PassThroughEncoding));

        It should_have_default_single_part_encoding_passthrough = () => defaultSinglePartEncoding.ShouldBeOfType(typeof(FluentJdf.Encoding.PassThroughEncoding));

        It should_have_default_multi_part_encoding_mime = () => defaultMultiPartEncoding.ShouldBeOfType(typeof(FluentJdf.Encoding.MimeEncoding));
    }
}
