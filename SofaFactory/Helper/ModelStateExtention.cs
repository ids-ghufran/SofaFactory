using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SofaFactory.Helper
{
	public static class ModelStateExtention
	{
		public static IDictionary ToSerializedDictionary(this ModelStateDictionary model)
		{
			return model.ToDictionary(
				k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage).ToArray());
		}
	}
}

