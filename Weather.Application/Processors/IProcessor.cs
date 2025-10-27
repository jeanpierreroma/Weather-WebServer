namespace Weather.Application.Processors;

public interface IProcessor<in TIn, out TOut>
{
    TOut Process(TIn raw);
}