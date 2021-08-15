using Collection;
namespace CSharpScript.File
{
	public interface UpdatableInstruction
	{
		void Update(AVL<int, Instruction> instructions, MetadataRoot metadata);
	}
}
