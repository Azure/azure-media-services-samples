// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Internal;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace System.ComponentModel.Composition
{
    internal static class MetadataViewProvider
    {
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static TMetadataView GetMetadataView<TMetadataView>(IDictionary<string, object> metadata)
        {
            Type metadataViewType = typeof(TMetadataView);

            // If the Metadata dictionary is cast compatible with the passed in type
            if (metadataViewType.IsAssignableFrom(typeof(IDictionary<string, object>)))
            {
                return (TMetadataView)metadata;
            }
            // otherwise is it a metadata view
            else
            {
                Type proxyType;
                if (metadataViewType.IsInterface)
                {
                    throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Strings.NotSupportedInterfaceMetadataView, metadataViewType.FullName));
                }
                else
                {
                    proxyType = metadataViewType;
                }

                // Now we have the type for the proxy create it
                try
                {
                    return (TMetadataView)proxyType.SafeCreateInstance(metadata);
                }
                catch (MissingMethodException ex)
                {
                    // Unable to create an Instance of the Metadata view '{0}' because a constructor could not be selected.  Ensure that the type implements a constructor which takes an argument of type IDictionary<string, object>.
                    throw new CompositionContractMismatchException(string.Format(CultureInfo.CurrentCulture,
                        Strings.CompositionException_MetadataViewInvalidConstructor,
                        proxyType.AssemblyQualifiedName), ex);
                }
            }
        }

        public static bool IsViewTypeValid(Type metadataViewType)
        {
            Assumes.NotNull(metadataViewType);

            // If the Metadata dictionary is cast compatible with the passed in type
            if (ExportServices.IsDefaultMetadataViewType(metadataViewType)
            ||  metadataViewType.IsInterface
            ||  ExportServices.IsDictionaryConstructorViewType(metadataViewType))
            {
                return true;
            }

            return false;
        }
    }
}
