﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for more information.

namespace Microsoft.VisualStudio.Extensibility.Testing.SourceGenerator.UnitTests
{
    using System.Collections.Immutable;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Testing;
    using Xunit;
    using VerifyCS = Microsoft.VisualStudio.Extensibility.Testing.SourceGenerator.UnitTests.Verifiers.CSharpSourceGeneratorVerifier<
        Microsoft.VisualStudio.Extensibility.Testing.SourceGenerator.TestServicesSourceGenerator>;

    public class TestServicesSourceGeneratorTests
    {
        private const string NullableAttributesSource = @"// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// <auto-generated/>

// This was copied from https://github.com/dotnet/runtime/blob/39b9607807f29e48cae4652cd74735182b31182e/src/libraries/System.Private.CoreLib/src/System/Diagnostics/CodeAnalysis/NullableAttributes.cs
// and updated to have the scope of the attributes be internal.
namespace System.Diagnostics.CodeAnalysis
{
#if !NETCOREAPP

    /// <summary>Specifies that null is allowed as an input even if the corresponding type disallows it.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    internal sealed class AllowNullAttribute : Attribute { }

    /// <summary>Specifies that null is disallowed as an input even if the corresponding type allows it.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
    internal sealed class DisallowNullAttribute : Attribute { }

    /// <summary>Specifies that an output may be null even if the corresponding type disallows it.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class MaybeNullAttribute : Attribute { }

    /// <summary>Specifies that an output will not be null even if the corresponding type allows it.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
    internal sealed class NotNullAttribute : Attribute { }

    /// <summary>Specifies that when a method returns <see cref=""ReturnValue""/>, the parameter may be null even if the corresponding type disallows it.</summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class MaybeNullWhenAttribute : Attribute
    {
        /// <summary>Initializes the attribute with the specified return value condition.</summary>
        /// <param name=""returnValue"">
        /// The return value condition. If the method returns this value, the associated parameter may be null.
        /// </param>
        public MaybeNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

        /// <summary>Gets the return value condition.</summary>
        public bool ReturnValue { get; }
    }

    /// <summary>Specifies that when a method returns <see cref=""ReturnValue""/>, the parameter will not be null even if the corresponding type allows it.</summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class NotNullWhenAttribute : Attribute
    {
        /// <summary>Initializes the attribute with the specified return value condition.</summary>
        /// <param name=""returnValue"">
        /// The return value condition. If the method returns this value, the associated parameter will not be null.
        /// </param>
        public NotNullWhenAttribute(bool returnValue) => ReturnValue = returnValue;

        /// <summary>Gets the return value condition.</summary>
        public bool ReturnValue { get; }
    }

    /// <summary>Specifies that the output will be non-null if the named parameter is non-null.</summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
    internal sealed class NotNullIfNotNullAttribute : Attribute
    {
        /// <summary>Initializes the attribute with the associated parameter name.</summary>
        /// <param name=""parameterName"">
        /// The associated parameter name.  The output will be non-null if the argument to the parameter specified is non-null.
        /// </param>
        public NotNullIfNotNullAttribute(string parameterName) => ParameterName = parameterName;

        /// <summary>Gets the associated parameter name.</summary>
        public string ParameterName { get; }
    }

    /// <summary>Applied to a method that will never return under any circumstance.</summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class DoesNotReturnAttribute : Attribute { }

    /// <summary>Specifies that the method will not return if the associated Boolean parameter is passed the specified value.</summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    internal sealed class DoesNotReturnIfAttribute : Attribute
    {
        /// <summary>Initializes the attribute with the specified parameter value.</summary>
        /// <param name=""parameterValue"">
        /// The condition parameter value. Code after the method will be considered unreachable by diagnostics if the argument to
        /// the associated parameter matches this value.
        /// </param>
        public DoesNotReturnIfAttribute(bool parameterValue) => ParameterValue = parameterValue;

        /// <summary>Gets the condition parameter value.</summary>
        public bool ParameterValue { get; }
    }

#endif

#if !NETCOREAPP || NETCOREAPP3_1

    /// <summary>Specifies that the method or property will ensure that the listed field and property members have not-null values.</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class MemberNotNullAttribute : Attribute
    {
        /// <summary>Initializes the attribute with a field or property member.</summary>
        /// <param name=""member"">
        /// The field or property member that is promised to be not-null.
        /// </param>
        public MemberNotNullAttribute(string member) => Members = new[] { member };

        /// <summary>Initializes the attribute with the list of field and property members.</summary>
        /// <param name=""members"">
        /// The list of field and property members that are promised to be not-null.
        /// </param>
        public MemberNotNullAttribute(params string[] members) => Members = members;

        /// <summary>Gets field or property member names.</summary>
        public string[] Members { get; }
    }

    /// <summary>Specifies that the method or property will ensure that the listed field and property members have not-null values when returning with the specified return value condition.</summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    internal sealed class MemberNotNullWhenAttribute : Attribute
    {
        /// <summary>Initializes the attribute with the specified return value condition and a field or property member.</summary>
        /// <param name=""returnValue"">
        /// The return value condition. If the method returns this value, the associated parameter will not be null.
        /// </param>
        /// <param name=""member"">
        /// The field or property member that is promised to be not-null.
        /// </param>
        public MemberNotNullWhenAttribute(bool returnValue, string member)
        {
            ReturnValue = returnValue;
            Members = new[] { member };
        }

        /// <summary>Initializes the attribute with the specified return value condition and list of field and property members.</summary>
        /// <param name=""returnValue"">
        /// The return value condition. If the method returns this value, the associated parameter will not be null.
        /// </param>
        /// <param name=""members"">
        /// The list of field and property members that are promised to be not-null.
        /// </param>
        public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
        {
            ReturnValue = returnValue;
            Members = members;
        }

        /// <summary>Gets the return value condition.</summary>
        public bool ReturnValue { get; }

        /// <summary>Gets field or property member names.</summary>
        public string[] Members { get; }
    }

#endif
}";

        private static readonly string BaseOutputDirectory = Path.GetDirectoryName(typeof(TestServicesSourceGeneratorTests).Assembly.Location);
        private static readonly string ExtensibilityTestingLibraryPath = Path.Combine(BaseOutputDirectory, "Current", "Microsoft.VisualStudio.Extensibility.Testing.Xunit.dll");
        private static readonly string ExtensibilityTestingLegacyLibraryPath = Path.Combine(BaseOutputDirectory, "Legacy", "Microsoft.VisualStudio.Extensibility.Testing.Xunit.dll");

        private static readonly MetadataReference ExtensibilityTestingLibrary = MetadataReference.CreateFromFile(
            ExtensibilityTestingLibraryPath,
            documentation: XmlDocumentationProvider.CreateFromFile(Path.ChangeExtension(ExtensibilityTestingLibraryPath, ".xml")));

        private static readonly MetadataReference ExtensibilityTestingLegacyLibrary = MetadataReference.CreateFromFile(
            ExtensibilityTestingLegacyLibraryPath,
            documentation: XmlDocumentationProvider.CreateFromFile(Path.ChangeExtension(ExtensibilityTestingLegacyLibraryPath, ".xml")));

        [Fact]
        public async Task TestGenerationForVS2022()
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net472.Default
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("Microsoft.VisualStudio.SDK", "17.0.31902.203"),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }

        [Fact]
        public async Task TestGenerationForVS2019_16_0()
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net472.Default
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("Microsoft.VisualStudio.SDK", "16.0.206"),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLegacyLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }

        [Fact]
        public async Task TestGenerationForVS2019_16_1()
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net472.Default
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("Microsoft.VisualStudio.SDK", "16.0.206"),
                            new PackageIdentity("Microsoft.VisualStudio.Shell.Framework", "16.1.28917.181"),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLegacyLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }

        [Fact]
        public async Task TestGenerationForVS2017()
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net46.Wpf
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("Microsoft.VisualStudio.SDK", "15.0.1"),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLegacyLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }

        [Theory]
        [InlineData("14.0.23205")]
        [InlineData("14.1.24720")]
        [InlineData("14.2.25123")]
        [InlineData("14.3.25407")]
        public async Task TestGenerationForVS2015(string shellVersion)
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net46.Wpf
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("Microsoft.VisualStudio.ComponentModelHost", "14.0.25424"),
                            new PackageIdentity("Microsoft.VisualStudio.Editor", shellVersion),
                            new PackageIdentity("Microsoft.VisualStudio.Shell.14.0", shellVersion),
                            new PackageIdentity("Microsoft.VisualStudio.Shell.Interop.14.0.DesignTime", shellVersion),
                            new PackageIdentity("Microsoft.VisualStudio.Text.UI.Wpf", shellVersion),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLegacyLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }

        [Fact]
        public async Task TestGenerationForVS2013()
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net46.Wpf
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("VSSDK.ComponentModelHost.12", "12.0.4"),
                            new PackageIdentity("VSSDK.Editor.12", "12.0.4"),
                            new PackageIdentity("VSSDK.Shell.12", "12.0.4"),
                            new PackageIdentity("VSSDK.Shell.Interop.11", "11.0.4"),
                            new PackageIdentity("VSSDK.Text.12", "12.0.4"),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLegacyLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }

        [Fact]
        public async Task TestGenerationForVS2012()
        {
            await new VerifyCS.Test
            {
                TestState =
                {
                    ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net46.Wpf
                        .AddPackages(ImmutableArray.Create(
                            new PackageIdentity("Microsoft.VisualStudio.Threading", "12.0.0"),
                            new PackageIdentity("VSSDK.ComponentModelHost.11", "11.0.4"),
                            new PackageIdentity("VSSDK.Editor.11", "11.0.4"),
                            new PackageIdentity("VSSDK.Shell.11", "11.0.4"),
                            new PackageIdentity("VSSDK.Shell.Interop.11", "11.0.4"),
                            //// Referencing Microsoft.VisualStudio.Shell.Interop.12.0 is required so the embedded
                            //// interop type IVsTaskSchedulerService2 can be checked at runtime.
                            new PackageIdentity("VSSDK.Shell.Interop.12", "12.0.4"),
                            new PackageIdentity("VSSDK.Text.11", "11.0.4"),
                            new PackageIdentity("xunit", "2.4.1"))),
                    AdditionalReferences =
                    {
                        ExtensibilityTestingLegacyLibrary,
                    },
                    Sources =
                    {
                        ("Nullable.cs", NullableAttributesSource),
                    },
                },
            }.AddGeneratedSources().RunAsync();
        }
    }
}
