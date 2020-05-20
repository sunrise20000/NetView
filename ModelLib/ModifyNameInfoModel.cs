using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib
{
	public class ModifyNameInfoModel
	{
		public int OldGlobalIndex { get; set; }
		public int OldLocalIndex { get; set; }
		public string ModuleName { get; set; }
		public int NewGlobalIndex { get; set; }
		public int NewLocalIndex { get; set; }
		public string OldName { get {
				return $"{ ModuleName}_{OldLocalIndex}";
			} }
		public string NewName { get {
				return $"{ ModuleName}_{NewLocalIndex}";
			} }
	}
}
