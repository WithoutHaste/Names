using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Names.Edit.MvcSite.Models.Home;

namespace Names.Edit.MvcSite.Models.ModelBinders
{
	public class MultipleNameDetailsBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if(bindingContext == null)
				throw new ArgumentNullException(nameof(bindingContext));

			List<EditNameDetail> results = new List<EditNameDetail>();

			int i = 0;
			while(true)
			{
				string prefix = "[" + i + "].";
				int? id = LoadOptionalInt(bindingContext, prefix + "NameDetailId");
				if(id == null)
					break;
				string origin = (string)bindingContext.ValueProvider.GetValue(prefix + "Origin").ConvertTo(typeof(string));
				string meaning = (string)bindingContext.ValueProvider.GetValue(prefix + "Meaning").ConvertTo(typeof(string));
				string isEdited = LoadOptionalString(bindingContext, prefix + "IsEdited");
				string isDeleted = LoadOptionalString(bindingContext, prefix + "IsDeleted");
				if(isEdited == "on" || isDeleted == "on")
				{
					results.Add(new EditNameDetail() {
						NameDetailId = id.Value,
						Origin = origin,
						Meaning = meaning,
						IsDeleted = (isDeleted == "on")
					});
				}
				i++;
			}

			return results.ToArray();
		}

		private string LoadOptionalString(ModelBindingContext bindingContext, string name)
		{
			ValueProviderResult result = bindingContext.ValueProvider.GetValue(name);
			if(result == null)
				return null;
			return (string) result.ConvertTo(typeof(string));
		}

		private int? LoadOptionalInt(ModelBindingContext bindingContext, string name)
		{
			ValueProviderResult result = bindingContext.ValueProvider.GetValue(name);
			if(result == null)
				return null;
			return (int)result.ConvertTo(typeof(int));
		}
	}
}