namespace Nexida.Infrastructure
{
	public interface IFactory<out T>
	{
		T Create();
	}
}