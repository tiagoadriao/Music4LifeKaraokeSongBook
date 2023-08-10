using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music4LifeKaraokeSongBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongbookController : ControllerBase
    {
        private readonly SongbookDbContext _songbookDbContext;
        private readonly CsvHandler csvHandler;

        public SongbookController(IConfiguration configuration, SongbookDbContext songbookDbContext)
        {
            _songbookDbContext = songbookDbContext;
            csvHandler = new CsvHandler(configuration, songbookDbContext);
        }

        // POST api/<SongbookController>
        [HttpPost("migration")]
        public async Task Post()
        {
            await csvHandler.MigrateCsvToDb();
        }
    }
}