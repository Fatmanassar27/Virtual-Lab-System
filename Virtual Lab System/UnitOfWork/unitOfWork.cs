using Virtual_Lab_System.Models;
using Virtual_Lab_System.Repository;

namespace Virtual_Lab_System.UnitOfWork
{
    public class unitOfWork
    {
        private readonly Context _db;

        private ExperimentRepository _experiment;
        private ReportRepository _report;
        private SubjectRepository _subject;

        public unitOfWork(Context db)
        {
            _db = db;
        }

        public SubjectRepository Subject
        {
            get
            {
                if (_subject == null)
                {
                    _subject = new SubjectRepository(_db);
                }
                return _subject;
            }
        }

        public ExperimentRepository Experiment
        {
            get
            {
                if (_experiment == null)
                {
                    _experiment = new ExperimentRepository(_db);
                }
                return _experiment;
            }
        }

        public ReportRepository Report
        {
            get
            {
                if (_report == null)
                {
                    _report = new ReportRepository(_db);
                }
                return _report;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
