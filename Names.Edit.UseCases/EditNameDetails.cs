using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;
using Names.Domain.Entities;
using Names.Edit.UseCases.Data;

namespace Names.Edit.UseCases
{
	public static class EditNameDetails
	{
		public static void Execute(INamesGateway gateway, EditNameDetailInput[] editInputs)
		{
			gateway.EditNameDetails(editInputs.Select(input => new EditNameDetailRequest() {
				NameDetailId = input.NameDetailId,
				Origin = input.Origin,
				Meaning = input.Meaning
			}).ToArray());
		}
	}
}
