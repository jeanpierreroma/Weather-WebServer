namespace Weather.Application;

public interface IProcessor<in TIn, out TOut>
{
    TOut Process(TIn raw);
}