using System;
namespace FourPlaces
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);

        // DependencyService.Get<IMessage>().ShortAlert(string message); 
    }
}
