using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Synergi.Licensing.Common
{
    [DataContract]
    public partial class ModulePackage : Record
    {
        #region Public Constructors

        public ModulePackage() : base() { }

        #endregion

        #region ColumnNames

        public static class ColumnNames
        {
            public const string ModulePackageId = "ModulePackageId";
            public const string ModuleId = "ModuleId";
            public const string PackageId = "PackageId";

        }

        #endregion

        #region Properties

        [DataMember]
        public int PackageModuleId { get { return Utilities.ToInt(RecordId); } set { RecordId = value; RaisePropertyChanged("PackageModuleId"); } }

        private int _moduleId;
        [DataMember]
        public int ModuleId { get { return _moduleId; } set { if (!object.ReferenceEquals(this.ModuleId, value)) { _moduleId = value; RaisePropertyChanged("ModuleId"); } } }

        private int _packageId;
        [DataMember]
        public int PackageId { get { return _packageId; } set { if (!object.ReferenceEquals(this.PackageId, value)) { _packageId = value; RaisePropertyChanged("PackageId"); } } }
        #endregion

    }
}
