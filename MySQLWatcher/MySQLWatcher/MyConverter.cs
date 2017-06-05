using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;  
using System.Globalization;


namespace MySQLWatcher
{
    class MyConverter : ExpandableObjectConverter
    {

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ListAttribute lst = (ListAttribute)context.PropertyDescriptor.Attributes[typeof(ListAttribute)]; StandardValuesCollection vals = new TypeConverter.StandardValuesCollection(lst.lists);
            return vals;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

    }

}
