using Progetto_Albergo.Models;

namespace Progetto_Albergo.Services
{
    public interface ICameraService
    {
        Camera GetCameraByNumero(int NumeroCamera);
        IEnumerable<Camera> GetAllCamere();
        void AddCamera(Camera camera);
    }
}
