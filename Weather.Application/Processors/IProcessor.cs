namespace Weather.Application.Abstraction;

public interface IProcessor<in TIn, out TOut>
{
    TOut Process(TIn raw);
}