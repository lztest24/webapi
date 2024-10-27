using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImgUri", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "admodum fabulae epicuro scriptorem perdiscere error texit maximi angusta facilis expetendum possint afferat integris imperitorum miraretur situm si comparaverit superstitione nullus admonitionem naturae artem voluptates claudicare a euripidis monstret", "/pictures/1.jpg", "tolerabiles dui expectata", 8489.11m },
                    { 2, "gravida rebus huc recusabo quietae pulcherrimum amice praeceptrice falsis inceptos summumque fructuosam iuberet interrogare fugiamus iucundum arbitratu concederetur inciderit", "/pictures/2.jpg", "pretium tradit temperantiam etsi", 3734.43m },
                    { 3, "mel error neglegentur via difficilem navigandi homero risus potiora videro amatoriis quo insolens aperiam finxerat compluribus morbis erat domesticarum operam certa putant libero sententia deterret falsis", "/pictures/3.jpg", "vulputate praetermittatur dicant", 4051.37m },
                    { 4, "oriantur minuti minus directam iste consequentis aristotelem iuvaret priventur alii artes opera laudabilis imperitos mortis alia acutum quippiam errem", "/pictures/4.jpg", "recte invidus ullus", 551.94m },
                    { 5, "quippe quaerendi putas suspicio tueri nemini fatendum quin laetitia habendus desiderent suis scaevola stoicis viris legum remotis lineam nisi aliter chorusque admodum illis obcaecati euripidis pacuvii", "/pictures/5.jpg", "plusque inventore modis praeclare", 3110.51m },
                    { 6, "curis voluptas sero incidunt placuit habendus exercitationem necesse reperiuntur inanium praetorem soliditatem accedunt aliquid gaudere consecutus curiosi tenent exorsus eae tempus urna veritus persecuti perspecta", "/pictures/6.jpg", "corrigere consumeret", 8748.69m },
                    { 7, "copiosae nostro praesentium deorsus detracto aliquet hendrerit credere musicis delectari delectat potest cupiditatibusque euripidis dixi adipiscendarum graecum doctus consuevit ipsis caritatem", "/pictures/7.jpg", "ferentur commodi mortis efficiendi", 201.41m },
                    { 8, "procedat turbulentaeque amentur cursu adolescens locum seditiones mens disseruerunt effluere se consuetudine ceteris", "/pictures/8.jpg", "suscipit sponte discordia aiunt", 447.86m },
                    { 9, "quorum graecis inducitur provincias veritus statue improborum renovata iudicia exercitationem praeterierunt singulos versuum data", "/pictures/9.jpg", "praetermittatur praeclarorum aristotele plus", 5943.6m },
                    { 10, "dedecora fere procedat accusator primus massa bonorum amorem persequeretur geometriaque compluribus saperet contrariis", "/pictures/10.jpg", "orationis disputationi maximasque admirer", 6497.61m },
                    { 11, "cursus asperner excepturi multo praesidium ferentur dixeris liber impendere solido vetuit disputando condimentum iudico corpus anteponant", "/pictures/11.jpg", "disserui quocirca", 3146.42m },
                    { 12, "admonitionem illis quid graecum chrysippi ceteris cupiditate terrore perturbari varius ultricies beateque fecisse antipatrum ipsos plus munere interpretum", "/pictures/12.jpg", "aliquando infantes offendimur", 9014.08m },
                    { 13, "reque mandamus sumus veniamus quaeso opera videntur reprehensione conscientia studuisse conversa paranda", "/pictures/13.jpg", "iuvaret iusteque exercitus persequeretur", 4227.49m },
                    { 14, "netus praetor tenuit aiebat conscientiam referta incurrunt philosophorum filio triarius minuit", "/pictures/14.jpg", "desiderare noctesque discenda", 9905.62m },
                    { 15, "optinere miserum mihi dicunt dicas confirmavit an vindicet concordia urbanitas intervalla suscepi saepti copulationesque gratissimo simplicem quale imperiis summa morbis quin bonum paria pueri", "/pictures/15.jpg", "confirmat uberius plerumque", 1208.82m },
                    { 16, "fuga accessio ullam interesset tria sumus officia amicitiae acri siculis interesset permanentes traditur suspicor deleniti fabulas malivoli suavitate", "/pictures/16.jpg", "duce illaberetur posidonium", 9840.82m },
                    { 17, "potest animumque cn utramque mauris tibique rudem quia vacuitate praeterita falli modis quantus ultimum loco impetu superstitione eligendi proin respirare putat miseram imitarentur", "/pictures/17.jpg", "mandaremus postremo", 3602.11m },
                    { 18, "gravissimis discedere contineret pertinacia petat elit summo fecisse videri factorum praesens difficilius dirigentur tamque principes ferentur fieri cn doloribus renovata class vocet tantalo repellere terentii titillaret consectetuer odia", "/pictures/18.jpg", "simulent depravatum intellegimus impetum", 4309.92m },
                    { 19, "habet soluta minuti labor corpus sumus assueverit pacto omnium atomorum tertium uterque aequitate momenti litteras perciperet perpetua maximo laudantium", "/pictures/19.jpg", "bonis felis", 5954.44m },
                    { 20, "hae falsarum fortunam quocirca quidem concupiscunt dixeris cognita sublata vivi ecce sensum fugiamus exquirere disputationi corrupti interiret amet necessitatibus consul allicit attento utamur quisquam exultat", "/pictures/20.jpg", "delectet nulli", 5061.92m },
                    { 21, "dolorem dixit quale assumenda cumanum consul restat captiosa esse libidinosarum haec velint aiebat velim prorsus probarent consuevit filium inimicus graeci altera", "/pictures/21.jpg", "invidiae utilitate parvos", 6631.04m },
                    { 22, "evertunt habent monstruosi instructus gustare parum eruditionem integer eo prima aegritudo artis eoque conquisitis atomorum fruenda fortitudinis probarentur atomus quamquam atomi animus viveremus aliam", "/pictures/22.jpg", "naturalem interpretum", 3734.79m },
                    { 23, "me deterruisset ornare erudito faciant stabilitas quantum difficilius optimus tite simplicem beate laudabilis probaturum dictum", "/pictures/23.jpg", "diligi class lectorem", 8850.98m },
                    { 24, "perdiderunt et liberae placebit patiatur docti hominum manus specie conspiratione", "/pictures/24.jpg", "partus stabilique deorsus fructuosam", 3975.19m },
                    { 25, "fuerit queo vel efficiat exeamus concedo posse scelerisque difficilem penitus molestias exercitumque quaeso esset tortor fortitudo tite confirmare effecerit studiis", "/pictures/25.jpg", "referuntur conversa primis", 5484.01m },
                    { 26, "perfunctio quis putet eveniet eripuit factorum acutus ponatur fructuosam synephebos timentis triario animadvertat sublatum veritus is eodem", "/pictures/26.jpg", "concursionibus necessitatibus pariendarum noster", 1656.89m },
                    { 27, "diu usus referri occultarum oriantur tamen sis neque diesque corporisque eriguntur pacuvii ullamcorper instituendarum tu arbitraretur totam graece dissentiunt etiamsi antiquis ulla", "/pictures/27.jpg", "beata ullum", 5290.16m },
                    { 28, "hac testibus indignae sicine mollitia mirari percipiatur probabis incurrunt secutus veriusque", "/pictures/28.jpg", "inopem vitam", 9386.88m },
                    { 29, "potuimus noster easque ceteris inane alios ignorant fruuntur operosam servata offendit modus", "/pictures/29.jpg", "operosam infimum consequi calere", 812.83m },
                    { 30, "menandro laudari usu corporum gloriatur munere praesens vindicet deleniti aeque breviter breviter distrahi principes repellere consequuntur vacuitate reperiri eveniet se possim videretur", "/pictures/30.jpg", "torqueantur ferant", 1907.2m },
                    { 31, "amarissimam contenta cognomen timidiores notissima aiebat atomorum turpe deorsus triarius putas commodo synephebos epicureum theseo iactant turbulenta exercendi clariora terroribus amorem iucundo notissima laudem", "/pictures/31.jpg", "studia aetatis iudex turma", 3468.06m },
                    { 32, "armatum consentaneum synephebos ostendit abest ullum alteram tamquam dapibus ista diam ornare solum usus afferat amicum crudelis uberius imperii arbitrer", "/pictures/32.jpg", "levis tua mortis", 6199.77m },
                    { 33, "percipitur mediocris maiores nostrum erudito causam bonae labore homero comprobavit", "/pictures/33.jpg", "ullum populo fuga", 995.03m },
                    { 34, "legerint propterea seditione temeritate doctrina cupiditate magnitudinem nati intelleges indoctis modo legendum libero virtutem comparandae ineruditus locatus obcaecati explicari gravissimis laetetur efficiat summo fusce cogitavisse consuetudine invidi", "/pictures/34.jpg", "sollicitudin ipsas suspicor fieri", 5033.29m },
                    { 35, "defendit amorem dedecora studio interrogare quasi laboriosam aegritudo unam contemnit sollicitare", "/pictures/35.jpg", "disseretur cupiditates plerumque amoris", 7004.69m },
                    { 36, "fecerit familiarem sentiunt militaris veritus futurove disciplinae solis interea quietus", "/pictures/36.jpg", "moveat videretur ii", 2875.75m },
                    { 37, "ego prima longinquitate industria explicatam turpius ulla menandro appetendi sit scientia frui medeam chorusque aenean sensus magnam", "/pictures/37.jpg", "ponunt praeclaram", 4966.45m },
                    { 38, "sententiae sociis istae causae sapienti restincto fabulae ii persius pendet ratione erit multos successionem complexiones ac consentinis ornare iracundia apud quis egestas vituperata probatus necesse sale coerceri sentit", "/pictures/38.jpg", "antiquis restincto", 6863.03m },
                    { 39, "insidiarum industriae statue angusti faciant latinas cohaerescent nocet consentientis generis cur alliciat architecto verentur diogenem supplicii senectus greges loca monet pronuntiaret class mentitum statim privatione laudem accusamus pertinaces", "/pictures/39.jpg", "disserendum torquate oritur", 8245.12m },
                    { 40, "gravissimis conducunt initia doloremque utramque sanguinem consequuntur fidem insipientiam occulta tempus reprehensione", "/pictures/40.jpg", "recta ingeniis putas", 154.07m },
                    { 41, "sitisque hae artes conturbamur facilius status abhorreant exiguam scaevolam laborat industria collegi formidinum tribuat imperitos teneam perpaulum scientia ratione obcaecati superstitio metus gaudere dignitatis eram", "/pictures/41.jpg", "maestitiam silano suscepi valetudinis", 9373.96m },
                    { 42, "consiliisque remissius locus accusamus motus fuga appetendum dicam reprimique minuti dolere officii volumus ostendit timeret exquisitaque effecerit vicinum grate reliquarum siculis afficit caeco extremo tantas epicureum adversa sentio statua", "/pictures/42.jpg", "pararetur arbitrer alienus", 4200.84m },
                    { 43, "officia naturales finitum ferae aperiam sentire interpretum aegritudo detraxit futuros dixeris miserius posuere expectamus verterunt detraxisse", "/pictures/43.jpg", "deterritum turbulentaeque eius sinat", 7620.29m },
                    { 44, "delicatissimi celeritas erigimur feugiat dicant fere vera fodere expeteretur fit conetur exercitumque efficit praesidium torquatum o explicabo valetudinis graviterque accusantium summis vacare optari", "/pictures/44.jpg", "perpetuis discidia class collegi", 274.78m },
                    { 45, "divitias amori primum detracto uberiora hominem dissident angere ponit nullus claris apeirian sola angoribus talem incurrunt huc amatoriis dissentias eros", "/pictures/45.jpg", "eruditi litteris tempus numquidnam", 8494.42m },
                    { 46, "cn rudem diuturnitatem intellegunt molestiae ancillae phaedrum inflammat factorum scribere ars exaudita huic bonas familiarem faciant pede molita nominis faciendum dum uterque affecti teneam", "/pictures/46.jpg", "fortunam egregios", 1172.46m },
                    { 47, "investigandi suppetet quamque a blanditiis cognitione consentientis quoquo involuta paria tribus percipit praeclara aliam videro morte", "/pictures/47.jpg", "eloquentiam tamquam iucundum pars", 345.2m },
                    { 48, "nullas iste propriae utamur inveneris corpus recta feci polyaeno epularum consiliisque deserunt quietus accuratius formidinum cursus maledici huc abest detractio cupiditatum suscepi causam utrum honeste multi dicturam vocet", "/pictures/48.jpg", "everti pleniorem", 3292.18m },
                    { 49, "poetae viverra conferebamus sane ludicra tractatas facillimis reprehensiones assentior cupiditate legatis praeceptrice tantopere postulet peccant referta amentur nulli euripidis iudicari praeteritas primus futuros referuntur manilium", "/pictures/49.jpg", "copiosae doleamus mucius quocirca", 5802.43m },
                    { 50, "metuamus improborum afficitur errore fugiendus amicitiae praeclare statua assiduitas successionem", "/pictures/50.jpg", "urbes reiciendis illo magnus", 2204.53m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
