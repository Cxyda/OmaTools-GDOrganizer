namespace Modules.O.M.A.Games.GDOrganizer.Editor.Utils
{
	/// <summary>
	/// TODO:
	/// </summary>
	public class CodeGenerationHelper
	{
		public static string GetLine(string content, int indentationLevel = 0)
		{
			var indentation = "";
			for (var i = 0; i < indentationLevel; i++)
			{
				indentation += "\t";
			}
			return indentation + content.Trim() + "\n";
		}
	}
}