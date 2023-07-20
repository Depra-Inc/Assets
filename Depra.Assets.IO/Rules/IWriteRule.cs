namespace Depra.Assets.IO.Rules
{
    public interface IWriteRule<in TData>
    {
        bool CanWrite();
        
        void Write(TData data);
    }
}