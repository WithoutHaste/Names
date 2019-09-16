using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Names.Domain;

namespace Names.Edit.UseCases
{
	public static class AddNickName
	{
		public static void Execute(INamesGateway gateway, string fullName, string nickName)
		{
			gateway.AddNickName(fullName, nickName);
		}
	}
}
