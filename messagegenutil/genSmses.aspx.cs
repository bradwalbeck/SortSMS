using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sortSMS_genSMSJson : System.Web.UI.Page
{

    /// Json data model:
    /// This is the model of data generated in this class. 
    /// {           
    ///    "date": "1422954605744",
    ///    "type": "1",
    ///    "body": "Hey there, it's me. What are you up to?",
    ///    "readable_date": "13 Jun 2015 11:52:31 AM",
    ///    "contact_name": "Heather"
    /// }

    protected void Page_Load(object sender, EventArgs e)
    {
        var data = buildJsonSMSData();
        jsonData.InnerText = data;
    }

    /// <summary>
    /// Builds json data set containing faux text messages. 
    /// </summary>
    /// <returns></returns>
    private string buildJsonSMSData()
    {
        StringBuilder smses = new StringBuilder();
        var corperateIpsumRemaining = corperateIpsum.Length;
        int timeStamp;
        string messageType;
        string contactName;
        string message;
        string formattedDateTime;

        smses.Append("[");
        for (var recordGenerationCounter = 0; recordGenerationCounter < corperateIpsumRemaining; recordGenerationCounter++)
        {
            timeStamp = generateTimeStamp();
            messageType = getMessageType();
            contactName = getRandomContact();
            message = getMessage(messageType, contactName);
            formattedDateTime = new DateTime(1970, 1, 1).AddSeconds(timeStamp).ToString("dd MMM yyyy hh:mm:ss tt");

            smses.Append("{");
            smses.Append("\"date\": \"" + timeStamp + "\",");
            smses.Append("\"type\": \"" + messageType + "\",");
            smses.Append("\"body\": \"" + message + "\",");
            smses.Append("\"readable_date\": \"" + formattedDateTime + "\",");
            smses.Append("\"contact_name\": \"" + contactName + "\"");
            smses.Append("},");
        }
        smses.Length--;
        smses.Append("]");
        return smses.ToString();
    }


    /// <summary>
    /// Uses random number generator to select a contact name.  
    /// </summary>
    /// <returns></returns>
    public string getRandomContact()
    {
        var random = getRandom();
        string[] contactNames = {
                               "Yuki Ebihara",
                               "Matt St. Millay",
                               "Scott Cup",
                               "Friedrich Nietzsche"
                           };
        if (random < 0.25)
        {
            return contactNames[0];
        }
        else if (random < 0.5)
        {
            return contactNames[1];
        }
        else if (random < 0.75)
        {
            return contactNames[2];
        }
        return contactNames[3];
    }

    /// <summary>
    /// Generates a random double. 
    /// </summary>
    /// <returns></returns>
    private double getRandom()
    {
        var generator = new RNGCryptoServiceProvider();
        var bytes = new Byte[8];
        generator.GetBytes(bytes);
        var unsignedInt = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
        return unsignedInt / (Double)(1UL << 53);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string getMessageType()
    {
        var random = getRandom();
        var sendPercentageThreshold = 0.55;
        var recievePercentageThreshold = 0.98;

        if (random < sendPercentageThreshold)
        {
            return "Sent";
        }
        if (random < recievePercentageThreshold)
        {
            return "Received";
        }
        return "Draft";
    }

    /// <summary>
    /// Randomly selects a message based on message type and contact. 
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="contact"></param>
    /// <returns></returns>
    public string getMessage(string messageType, string contact)
    {
        var random = getRandom();
        if (messageType.Equals("Sent") || messageType.Equals("Draft"))
        {
            return corperateIpsum[Math.Abs(((int)(Math.Ceiling(random * corperateIpsum.Length)))) - 1];
        }
        switch (contact)
        {
            case "Yuki Ebihara":
                return officeIpsum[Math.Abs(((int)(random * officeIpsum.Length)) - 1)];
            case "Matt St. Millay":
                return metaphorIpsum[Math.Abs(((int)(random * metaphorIpsum.Length)) - 1)];
            case "Scott Cup":
                return clientIpsum[Math.Abs(((int)(random * clientIpsum.Length)) - 1)];
            case "Friedrich Nietzsche":
                return neitzscheIpsom[Math.Abs(((int)(random * neitzscheIpsom.Length)) - 1)];
            default:
                return "";
        }
    }

    /// <summary>
    /// generates a timestamp within the last year-ish of the writing of this function. 
    /// </summary>
    /// <returns></returns>
    public int generateTimeStamp()
    {
        var random = getRandom();
        var timeRangeFloor = 1400544000;
        var timeRangeCoefficent = 34789752;
        return timeRangeFloor + (int)(random * timeRangeCoefficent);
    }
    /// <summary>
    /// http://officeipsum.com/
    /// </summary>
    public static string[] officeIpsum = { 
        "Pipeline pushback."
        ,"Programmatically."
        ,"Baseline the procedure and samepage your department golden goose."
        ,"That jerk from finance really threw me under the bus nail jelly to the hothouse wall drink the Kool-aid bells and whistles prethink accountable talk nor hit the ground running."
        ,"Open door policy come up with something buzzworthy on your plate, so run it up the flagpole."
        ,"Table the discussion we need to dialog around your choice of work attire, cloud strategy hard stop helicopter view strategic staircase."
        ,"Drink the Kool-aid. UX we need more paper. Going forward run it up the flagpole, or turd polishing, so out of the loop cannibalize."
        ,"Collaboration through advanced technlogy one-sheet due diligence feature creep, and high turnaround rate for beef up cross-pollination."
        ,"Screw the pooch level the playing field, but knowledge process outsourcing. Execute quick win into the weeds, and strategic staircase."
        ,"Price point put a record on and see who dances knowledge process outsourcing feature creep, so wheelhouse, for moving the goalposts. Time to open the kimono get all your ducks in a row."
        ,"Pipeline player-coach for forcing function yet killing it."
        ,"Are we in agreeance vertical integration message the initiative, yet future-proof no scraps hit the floor moving the goalposts."
        ,"Close the loop quick win, or we need more paper and are we in agreeance what do you feel you would bring to the table if you were hired for this position imagineer high-level."
        ,"Face time cannibalize, turd polishing, nor beef up, so action item. High-level wheelhouse idea shower come up with something buzzworthy, and face time."
        ,"It just needs more cowbell show pony race without a finish line close the loop after I ran into Helen at a restaurant, I realized she was just office pretty, yet data-point, or bleeding edge."
        ,"Reach out take five, punch the tree, and come back in here with a clear head, so best practices, so market-facing, or bells and whistles. Out of the loop future-proof."
        ,"That jerk from finance really threw me under the bus work usabiltiy move the needle organic growth root-and-branch review."
	};

    /// <summary>
    /// http://metaphorpsum.com/
    /// </summary>
    public static string[] metaphorIpsum = {
        "As far as we can estimate, the august of an instruction becomes a racy input."
        ,"They were lost without the undone toe that composed their congo."
        ,"Leathers are righteous oysters. As far as we can estimate, a clam is a street's bookcase."
        ,"A drive is a cd's soccer. Those bells are nothing more than wars."
        ,"In ancient times authors often misinterpret the heron as a laden garage, when in actuality it feels more like a piscine dedication."
        ,"This is not to discredit the idea that the first costive tub is, in its own way, a bead."
        ,"One cannot separate twines from unshamed lans."
        ,"Their base was, in this moment, an excused wealth."
        ,"The cement is a bus."
        ,"The legal is a veterinarian."
        ,"Their twist was, in this moment, a nicer leek."
        ,"The literature would have us believe that a thickset michael is not but a year."
        ,"In ancient times parotid parents show us how stocks can be newsprints."
        ,"In modern times genty guns show us how acrylics can be harmonicas."
        ,"The queen is a theater. Those mirrors are nothing more than cardigans."
        ,"The tailing army reveals itself as an unfooled drizzle to those who look."
        ,"In modern times a grimy anger's panther comes with it the thought that the obese sprout is a clave."
	};

    /// <summary>
    /// http://officeipsum.com/
    /// </summary>
    public static string[] clientIpsum = {
         "How much will it cost can we try some other colours maybe, yet this looks perfect. Just Photoshop out the dog, add a baby, and make the curtains blue, and I have an awesome idea for a startup, i need you to build it for me."
        ,"Jazz it up a little something summery; colourful, but could you rotate the picture to show the other side of the room? other agencies charge much lesser I know somebody who can do this for a reasonable cost."
        ,"Make it pop make it look like Apple I like it, but can the snow look a little warmer. I need a website. How much will it cost just do what you think."
        ,"I trust you I need a website."
        ,"How much will it cost, and I really like the colour but can you change it other agencies charge much lesser, yet could you rotate the picture to show the other side of the room? so the flier should feel like a warm handshake."
        ,"Make it sexy can we have another option, and this is just a 5 minutes job I need a website. How much will it cost, and I think we need to start from scratch, nor something summery; colourful."
        ,"Thanks for taking the time to make the website, but i already made it in wix theres all this spanish text on my site make it pop there are more projects lined up charge extra the next time will royalties in the company do instead of cash."
        ,"Can you make it stand out more? I like it, but can the snow look a little warmer, yet we exceed the clients' expectations can you turn it around in photoshop so we can see more of the front, so can you put 'find us on facebook' by the facebook logo?"
        ,"You might wanna give it another shot we are a big name to have in your portfolio is this the best we can do, yet we are a non-profit organization."
        ,"Can you rework to make the pizza look more delicious."
        ,"It looks a bit empty, try to make everything bigger we are a startup needs to be sleeker can you make it stand out more? can you use a high definition screenshot, the website doesn't have the theme i was going for."
        ,"We exceed the clients' expectations can it be more retro, yet can you turn it around in photoshop so we can see more of the front, but this looks perfect. Just Photoshop out the dog, add a baby, and make the curtains blue."
        ,"The hair is just too polarising. Something summery; colourful that sandwich needs to be more playful just do what you think. I trust you, so I want you to take it to the next level, but can it be more retro, or make it pop."
        ,"Thanks for taking the time to make the website, but i already made it in wix theres all this spanish text on my site, the website doesn't have the theme i was going for. Just do what you think."
        ,"I trust you."
        ,"Can you rework to make the pizza look more delicious give us a complimentary logo along with the website, for there are more projects lined up charge extra the next time."
        ,"I know somebody who can do this for a reasonable cost I know somebody who can do this for a reasonable cost."
        ,"Is this the best we can do make it original, so try a more powerful colour."
        ,"Can you make pink a little more pinkish thanks for taking the time to make the website, but i already made it in wix."
        ,"Jazz it up a little. This is just a 5 minutes job make it sexy, so other agencies charge much lesser concept is bang on, but can we look at a better execution, so the flier should feel like a warm handshake, something summery; colourful, or I really like the colour but can you change it."
        ,"Can you pimp this powerpoint, need more geometry patterns needs to be sleeker, so can we have another option, for could you do an actual logo instead of a font, nor can you pimp this powerpoint, need more geometry patterns or we have big contacts we will promote you. We exceed the clients' expectations."
        ,"We are a non-profit organization thanks for taking the time to make the website, but i already made it in wix i was wondering if my cat could be placed over the logo in the flyer something summery; colourful, nor low resolution?"
        ,"It looks ok on my screen."
        ,"Im not sure, try something else."
        ,"Make it look like Apple can we have another option could you do an actual logo instead of a font I want you to take it to the next level."
        ,"I like it, but can the snow look a little warmer I really like the colour but can you change it, so we are a non-profit organization try a more powerful colour."
    };

    /// <summary>
    /// http://nietzsche-ipsum.com/
    /// </summary>
    public static string[] neitzscheIpsom = { 
         "Aversion philosophy sexuality of contradict convictions good salvation ubermensch chaos decrepit war justice reason."
        ,"Strong play contradict mountains sexuality hatred right joy evil enlightenment transvaluation."
        ,"Grandeur merciful spirit justice hatred abstract ascetic morality pinnacle spirit. Burying grandeur pinnacle horror battle ideal."
        ,"Law hatred god salvation of against burying war society snare prejudice snare overcome evil. Aversion selfish victorious ultimate gains enlightenment war faith of abstract zarathustra value hope."
        ,"Grandeur strong christian oneself society transvaluation good decieve superiority overcome merciful intentions. Pinnacle superiority transvaluation salvation spirit intentions depths passion."
        ,"Revaluation snare gains good aversion ultimate gains horror."
        ,"Law ideal joy chaos ocean law suicide ocean victorious enlightenment battle reason virtues. Against virtues chaos snare overcome burying intentions passion deceptions."
        ,"Christian joy right will ultimate superiority revaluation derive. Value holiest self burying of. Truth christianity intentions free selfish salvation revaluation."
        ,"Good sea burying suicide christian hope insofar aversion spirit horror burying ultimate overcome."
        ,"Madness intentions good madness christian endless deceptions hatred suicide faith. Snare overcome evil reason victorious hope transvaluation reason hope battle law christianity."
        ,"Marvelous free eternal-return selfish justice god good prejudice."
        ,"Good victorious convictions selfish derive chaos faith merciful snare evil marvelous. Love philosophy prejudice free morality zarathustra sexuality zarathustra inexpedient hope oneself disgust."
        ,"Justice salvation eternal-return aversion intentions deceptions merciful morality mountains derive chaos."
        ,"God philosophy intentions ascetic spirit horror christianity horror pinnacle revaluation will society. Joy contradict moral pinnacle marvelous spirit joy zarathustra truth christian superiority christianity battle overcome."
        ,"Suicide of merciful christianity suicide dead contradict disgust reason transvaluation moral noble. Self inexpedient overcome battle revaluation passion convictions convictions horror."
        ,"Grandeur justice passion convictions strong revaluation gains justice. Joy passion battle self inexpedient ideal fearful fearful."
        ,"Christianity deceptions eternal-return reason society passion zarathustra disgust convictions morality chaos chaos overcome. Christian deceptions horror evil strong selfish deceptions."
        ,"Convictions ubermensch enlightenment derive aversion truth salvation war battle horror reason."
        ,"Aversion zarathustra reason madness sexuality ocean free virtues endless self hatred play moral overcome. God ultimate reason zarathustra war endless."
        ,"Transvaluation decieve justice marvelous overcome faith battle selfish against contradict ideal. Ultimate salvation merciful suicide will merciful snare ascetic faith suicide suicide evil."
        ,"Moral eternal-return zarathustra noble will oneself passion god free. Hope inexpedient revaluation chaos spirit strong noble free christianity christianity ultimate depths."
        ,"Ubermensch enlightenment transvaluation grandeur play virtues christian eternal-return marvelous intentions sea salvation dead. Pinnacle virtues overcome pinnacle selfish disgust joy contradict love faith morality."
        ,"Mountains depths ultimate revaluation pious truth pinnacle mountains god eternal-return reason gains. Morality spirit intentions decrepit fearful zarathustra pinnacle love ascetic aversion virtues."
        ,"Self merciful deceptions superiority salvation pious. Faith god self virtues deceptions moral play. Eternal-return selfish joy ubermensch deceptions justice value victorious suicide disgust transvaluation abstract."
        ,"Hope against free intentions joy derive overcome suicide marvelous enlightenment law snare aversion. Eternal-return ubermensch play chaos play self horror abstract battle."
        ,"Battle battle hope revaluation decieve evil deceptions noble hatred joy faithful. Value merciful insofar decrepit burying self. Depths victorious will suicide god god faith insofar dead contradict."
        ,"Enlightenment reason gains ascetic justice value joy enlightenment eternal-return joy intentions truth strong. Pinnacle suicide passion free ascetic victorious pious society marvelous virtues."
        ,"Inexpedient free sexuality snare fearful mountains selfish burying faithful enlightenment law war hope madness."
        ,"Insofar holiest salvation reason value prejudice ultimate faithful revaluation transvaluation horror moral decieve. Hatred value superiority madness evil disgust noble play justice morality noble revaluation endless overcome."
        ,"Truth will revaluation spirit revaluation transvaluation good chaos pious."
        ,"Mountains god victorious value reason sexuality. Holiest contradict zarathustra superiority virtues horror revaluation right sea law spirit. Strong reason mountains virtues dead sexuality society love christian suicide."
        ,"Intentions moral merciful hatred sea virtues overcome reason sexuality contradict oneself gains oneself decrepit. Dead victorious snare law war good selfish. Ultimate madness strong joy truth depths faith truth strong."
        ,"Madness war pious deceptions suicide ocean evil."
        ,"Society ideal god ultimate burying fearful ultimate law abstract. Salvation ascetic fearful overcome will holiest philosophy endless gains aversion madness."
        ,"Virtues gains salvation disgust chaos god intentions will disgust deceptions."
        ,"Revaluation prejudice virtues ultimate truth hatred victorious prejudice will snare christianity morality. Zarathustra enlightenment philosophy evil disgust dead abstract ultimate."
        ,"Chaos evil virtues battle grandeur moral enlightenment will christianity philosophy against sea. Prejudice eternal-return moral god pinnacle faith inexpedient hope. Hope grandeur play horror disgust."
        ,"Philosophy ubermensch value decrepit derive enlightenment moral decieve truth contradict moral love endless sea. Hatred madness chaos merciful justice marvelous ubermensch burying christian free faithful selfish holiest."
        ,"Spirit oneself marvelous battle gains. Superiority merciful christianity virtues marvelous pinnacle ocean moral deceptions god."
        ,"Moral good justice holiest pious gains ultimate. Spirit ocean decrepit ideal derive hatred. Virtues suicide hatred pinnacle ocean hatred."
        ,"Ideal spirit sea society contradict right will."
        ,"Transvaluation eternal-return ultimate grandeur burying christian decieve intentions superiority. Ultimate joy eternal-return transvaluation strong abstract prejudice god noble right."
        ,"Oneself decrepit society spirit passion chaos hatred. Derive of self deceptions superiority philosophy zarathustra inexpedient good ocean faithful play."
        ,"Strong holiest spirit burying strong aversion derive spirit holiest. Christianity self virtues morality inexpedient decrepit mountains."
        ,"Noble value superiority moral ascetic passion. Enlightenment burying gains burying victorious revaluation ascetic virtues will intentions."
        ,"Salvation war pinnacle love war."
        ,"Pious philosophy oneself mountains enlightenment noble selfish intentions revaluation."
        ,"Endless zarathustra salvation gains marvelous society contradict passion christianity hope decieve deceptions. Intentions salvation gains ultimate aversion god hatred oneself hope ultimate salvation sea war madness."
        ,"Sexuality ocean faithful oneself pious grandeur evil philosophy ultimate noble horror holiest. Marvelous god pious hatred battle joy reason christianity self revaluation play hope derive."
    };

    /// <summary>
    /// http://www.cipsum.com/
    /// </summary>
    public static string[] corperateIpsum = {
        "Collaboratively administrate empowered markets via plug-and-play networks."
        ,"Dynamically procrastinate B2C users after installed base benefits."
        ,"Dramatically visualize customer directed convergence without revolutionary ROI."
        ,"Efficiently unleash cross-media information without cross-media value."
        ,"Quickly maximize timely deliverables for real-time schemas."
        ,"Dramatically maintain clicks-and-mortar solutions without functional solutions."
        ,"Completely synergize resource taxing relationships via premier niche markets."
        ,"Professionally cultivate one-to-one customer service with robust ideas."
        ,"Dynamically innovate resource-leveling customer service for state of the art customer service."
        ,"Objectively innovate empowered manufactured products whereas parallel platforms."
        ,"Holisticly predominate extensible testing procedures for reliable supply chains."
        ,"Dramatically engage top-line web services vis-a-vis cutting-edge deliverables."
        ,"Proactively envisioned multimedia based expertise and cross-media growth strategies."
        ,"Seamlessly visualize quality intellectual capital without superior collaboration and idea-sharing."
        ,"Holistically pontificate installed base portals after maintainable products."
        ,"Phosfluorescently engage worldwide methodologies with web-enabled technology. Interactively coordinate proactive e-commerce via process-centric outside the box thinking."
        ,"Completely pursue scalable customer service through sustainable potentialities."
        ,"Collaboratively administrate turnkey channels whereas virtual e-tailers. Objectively seize scalable metrics whereas proactive e-services."
        ,"Seamlessly empower fully researched growth strategies and interoperable internal or organic sources."
        ,"Credibly innovate granular internal or organic sources whereas high standards in web-readiness."
        ,"Energistically scale future-proof core competencies vis-a-vis impactful experiences."
        ,"Dramatically synthesize integrated schemas with optimal networks."
        ,"Interactively procrastinate high-payoff content without backward-compatible data. Quickly cultivate optimal processes and tactical architectures."
        ,"Completely iterate covalent strategic theme areas via accurate e-markets."
        ,"Globally incubate standards compliant channels before scalable benefits. Quickly disseminate superior deliverables whereas web-enabled applications."
        ,"Quickly drive clicks-and-mortar catalysts for change before vertical architectures."
        ,"Credibly reintermediate backend ideas for cross-platform models. Continually reintermediate integrated processes through technically sound intellectual capital."
        ,"Holistically foster superior methodologies without market-driven best practices."
        ,"Distinctively exploit optimal alignments for intuitive bandwidth. Quickly coordinate e-business applications through revolutionary catalysts for change."
        ,"Seamlessly underwhelm optimal testing procedures whereas bricks-and-clicks processes."
        ,"Synergistically evolve 2.0 technologies rather than just in time initiatives. Quickly deploy strategic networks with compelling e-business."
        ,"Credibly pontificate highly efficient manufactured products and enabled data."
        ,"Dynamically target high-payoff intellectual capital for customized technologies. Objectively integrate emerging core competencies before process-centric communities."
        ,"Dramatically evisculate holistic innovation rather than client-centric data."
        ,"Progressively maintain extensive infomediaries via extensible niches."
        ,"Dramatically disseminate standardized metrics after resource-leveling processes."
        ,"Objectively pursue diverse catalysts for change for interoperable meta-services."
        ,"Proactively fabricate one-to-one materials via effective e-business."
        ,"Completely synergize scalable e-commerce rather than high standards in e-services. Assertively iterate resource maximizing products after leading-edge intellectual capital."
        ,"Distinctively re-engineer revolutionary meta-services and premium architectures."
        ,"Intrinsically incubate intuitive opportunities and real-time potentialities. Appropriately communicate one-to-one technology after plug-and-play networks."
        ,"Quickly aggregate B2B users and worldwide potentialities. Progressively plagiarize resource-leveling e-commerce through resource-leveling core competencies."
        ,"Dramatically mesh low-risk high-yield alignments before transparent e-tailers."
        ,"Appropriately empower dynamic leadership skills after business portals."
        ,"Globally myocardinate interactive supply chains with distinctive quality vectors."
        ,"Globally revolutionize global sources through interoperable services."
        ,"Enthusiastically mesh long-term high-impact infrastructures vis-a-vis efficient customer service. Professionally fashion wireless leadership rather than prospective experiences."
        ,"Energistically myocardinate clicks-and-mortar testing procedures whereas next-generation manufactured products."
        ,"Dynamically reinvent market-driven opportunities and ubiquitous interfaces."
        ,"Energistically fabricate an expanded array of niche markets through robust products. Appropriately implement visionary e-services vis-a-vis strategic web-readiness."
        ,"Compellingly embrace empowered e-business after user friendly intellectual capital."
        ,"Interactively actualize front-end processes with effective convergence. Synergistically deliver performance based methods of empowerment whereas distributed expertise."
        ,"Efficiently enable enabled sources and cost effective products. Completely synthesize principle-centered information after ethical communities."
        ,"Efficiently innovate open-source infrastructures via inexpensive materials."
        ,"Objectively integrate enterprise-wide strategic theme areas with functionalized infrastructures. Interactively productize premium technologies whereas interdependent quality vectors."
        ,"Rapaciously utilize enterprise experiences via 24/7 markets."
        ,"Uniquely matrix economically sound value through cooperative technology."
        ,"Competently parallel task fully researched data and enterprise process improvements. Collaboratively expedite quality manufactured products via client-focused results."
        ,"Quickly communicate enabled technology and turnkey leadership skills."
        ,"Uniquely enable accurate supply chains rather than frictionless technology. Globally network focused materials vis-a-vis cost effective manufactured products."
        ,"Enthusiastically leverage existing premium quality vectors with enterprise-wide innovation."
        ,"Phosfluorescently leverage others enterprise-wide outside the box thinking with e-business collaboration and idea-sharing."
        ,"Proactively leverage other resource-leveling convergence rather than inter-mandated networks."
        ,"Rapaciously seize adaptive infomediaries and user-centric intellectual capital."
        ,"Collaboratively unleash market-driven outside the box thinking for long-term high-impact solutions. Enthusiastically engage fully tested process improvements before top-line platforms."
        ,"Efficiently myocardinate market-driven innovation via open-source alignments."
        ,"Dramatically engage high-payoff infomediaries rather than client-centric imperatives. Efficiently initiate world-class applications after client-centric infomediaries."
        ,"Phosfluorescently expedite impactful supply chains via focused results."
        ,"Holistically generate open-source applications through bleeding-edge sources. Compellingly supply just in time catalysts for change through top-line potentialities."
        ,"Uniquely deploy cross-unit benefits with wireless testing procedures."
        ,"Collaboratively build backward-compatible relationships whereas tactical paradigms."
        ,"Compellingly reconceptualize compelling outsourcing whereas optimal customer service."
        ,"Quickly incentivize impactful action items before tactical collaboration and idea-sharing."
        ,"Monotonically engage market-driven intellectual capital through wireless opportunities. Progressively network performance based services for functionalized testing procedures."
        ,"Globally harness multimedia based collaboration and idea-sharing with backend products. Continually whiteboard superior opportunities via covalent scenarios."
    };
}