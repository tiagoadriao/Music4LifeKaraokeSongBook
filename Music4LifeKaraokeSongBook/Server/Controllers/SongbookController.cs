using Microsoft.AspNetCore.Mvc;
using Music4LifeKaraokeSongBook.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music4LifeKaraokeSongBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongbookController : ControllerBase
    {
        private readonly SongbookDbContext _songbookDbContext;
        private readonly CsvHandler csvHandler;
        private readonly DbHandler dbHandler;

        public SongbookController(IConfiguration configuration, SongbookDbContext songbookDbContext)
        {
            _songbookDbContext = songbookDbContext;
            csvHandler = new CsvHandler(configuration, songbookDbContext);
            dbHandler = new DbHandler(configuration, songbookDbContext);
        }

        // GET: api/<SongbookController>
        [HttpGet]
        public List<SongDb> Get()
        {
            return dbHandler.GetAllSongs();
        }

        // GET api/<SongbookController>/5
        [HttpGet("Language/{language}")]
        public List<SongDb> GetByLanguageId(Language language)
        {
            return dbHandler.GetAllSongsByLanguage(language);
        }

        // GET api/<SongbookController>/5
        [HttpGet("Singer/{singername}")]
        public List<SongDb> GetBySinger(string singerName)
        {
            return dbHandler.GetAllSongsBySinger(singerName);
        }

        // POST api/<SongbookController>
        [HttpPost("migration")]
        public async Task Post()
        {
          await csvHandler.MigrateCsvToDb();
        }

        // PUT api/<SongbookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SongbookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
