namespace Grilo.Shared.Utils
{
    public interface IUseCase<I, O>
    {
        Task<Result<O?>> Execute(I input);
    }
    public interface IUseCase<O>
    {
        Task<Result<O?>> Execute();
    }
}