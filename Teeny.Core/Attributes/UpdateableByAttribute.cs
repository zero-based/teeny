using System;

namespace Teeny.Core.Attributes
{
    public class UpdateableByAttribute : Attribute
    {
        public ScannerStateType UpdatingStateType;
        public UpdateableByAttribute(ScannerStateType updatingStateType)
        {
            UpdatingStateType = updatingStateType;
        }
    }
}
