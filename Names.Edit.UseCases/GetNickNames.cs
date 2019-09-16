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
	public static class GetNickNames
	{
		public static NickNameOutput[] Execute(INamesGateway gateway)
		{
			return gateway.GetNickNames().Select(record => new NickNameOutput() {
				FullNameId = record.FullNameId,
				FullName = record.FullName.Name,
				NickNameId = record.NickNameId,
				NickName = record.NickName.Name
			}).ToArray();
		}
	}
}
