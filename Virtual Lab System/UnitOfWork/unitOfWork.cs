using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.Repository;

namespace Virtual_Lab_System.UnitOfWork
{
    public class unitOfWork
    {
        public readonly Context _db;
        public readonly IMapper _mapper;
        UserManager<ApplicationUser> _userManager;
        public readonly IWebHostEnvironment _env;

        private ExperimentRepository _experiment;
        private ReportRepository _report;
        private SubjectRepository _subject;
        private UserRepository _user;

        public unitOfWork(Context db, IMapper mapper, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _env = env;
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
        public UserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_userManager,_mapper);
                }
                return _user;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
