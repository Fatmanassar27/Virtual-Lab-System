using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public interface IExperimentRepository
    {
        Task<List<Experiment>> GetAll(int page, int pageSize, string search);
        Task<int> GetTotalCount(string search);
        Task<Experiment?> GetById(int id);
        Task<Experiment> Add(Experiment experiment);
        Task<Experiment?> Update(int id, Experiment updated);
        Task<bool> Delete(int id);
        Task<string?> UploadPdf(int id, IFormFile file, string webRootPath);
    }
}
