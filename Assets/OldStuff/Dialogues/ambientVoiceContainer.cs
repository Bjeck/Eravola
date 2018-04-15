using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ambientInst : Inst {
	public float ambDelay = 0;

}


public class ambientVoiceContainer : MonoBehaviour {
	public static ambientVoiceContainer instance { get; private set; }

	public List<ambientInst> ambientContainer = new List<ambientInst>();
	
	public Dictionary<string, List<ambientInst>> allAmbients = new Dictionary<string, List<ambientInst>>();


	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);

		#region firstVillage
	//FIRST VILLAGE INTRO
		ambientInst firstVillage = new ambientInst ();
		firstVillage.id = 0;
		firstVillage.response = "'What?' ¤'Yes. He's coming today.'¤'When?' ¤'Don't know. Soon.'";
		firstVillage.thoughts = "I live in a small hovel on the outskirts of Caudden City, far enough away that you can’t smell the shit but still close enough that the chimneys line the sky.";
		firstVillage.thoughtsDelay = 1.5f;
		ambientContainer.Add (firstVillage);

		ambientInst firstVillage2 = new ambientInst ();
		firstVillage2.id = 1;
		firstVillage2.response = "'Well, I better go tell Rasmin.¤'And I gotta prepare. See you at the inn.'";
		firstVillage2.thoughts = "The most exciting thing that happens in this town is when the smoke changes colour because of a mage experiment gone wrong in the city.";
		firstVillage2.thoughtsDelay = 1.5f;
		
		ambientContainer.Add (firstVillage2);

		ambientInst firstVillage3 = new ambientInst ();
		firstVillage3.id = 2;
		firstVillage3.disengage = true;
		firstVillage3.NextTrigger("innIntro",false);
		ambientContainer.Add (firstVillage3);

		allAmbients.Add ("firstVillage", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region innIntro
	//INN INTRO
		ambientInst innIntro1 = new ambientInst ();
		innIntro1.id = 0;
		innIntro1.response = "";
		innIntro1.thoughts = "There's already a crowd gathering in the inn.  ¤Nearly half the village is here, talking loudly about wild rumours, misinforming, and just generally enjoying themselves. ";
		ambientContainer.Add (innIntro1);

		ambientInst innIntro2 = new ambientInst ();
		innIntro2.id = 1;
		innIntro2.response = "\'Come to see the show?\' <he asks.>";
		innIntro2.ambDelay = 2.5f;
		innIntro2.thoughts = "Me and Ceara scout Illij sitting by a corner table alone as he usually does. ¤We sit down next to him before he even notices us.";
		ambientContainer.Add (innIntro2);

		ambientInst exit = new ambientInst ();
		exit.id = 2;
		exit.disengage = true;
		exit.NextTrigger("IllijIntro",true);
		ambientContainer.Add (exit);

		allAmbients.Add ("innIntro", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region sageIntro
		ambientInst sageIntro1 = new ambientInst ();
		sageIntro1.id = 0;
		sageIntro1.response = "'He's here!'";
		sageIntro1.thoughts = "I'm interrupted by the door opening and someone shouting.";
		sageIntro1.ambDelay = 1.5f;
		ambientContainer.Add (sageIntro1);

		ambientInst sageIntro2 = new ambientInst ();
		sageIntro2.id = 1;
		sageIntro2.response = "'What's he look like, you think?'¤'Dammit, I forgot my purse'¤'What do you--'";
		sageIntro2.thoughts = "Murmur erupts and dies immediately as more people enter the inn, followed lastly by someone I don't know.";
		ambientContainer.Add (sageIntro2);

		ambientInst sageIntr3 = new ambientInst ();
		sageIntr3.id = 2;
		sageIntr3.response = "";
		sageIntr3.thoughts = "He's tall, got dark skin that's still lighter than Illij's, as if it was a mix of the two.  ¤His clothes are strange, red and brown, with green lines running down in a design that must have taken forever to sew.   ¤He walks in and looks around, everyone else staring up at him as if he's their long lost child who came home after war.";
		ambientContainer.Add (sageIntr3);

		ambientInst sageIntr4 = new ambientInst ();
		sageIntr4.id = 3;
		sageIntr4.response = "'Hello' <he says. He crooks a smile.>";
		sageIntr4.thoughts = "He looks, all in all, like an asshole.         ¤No, looking at his clothes, he looks like a rich asshole.";
		sageIntr4.thoughtsDelay = 1f;
		ambientContainer.Add (sageIntr4);

		ambientInst sageIntr5 = new ambientInst ();
		sageIntr5.id = 4;
		sageIntr5.response = "";
		sageIntr5.thoughts = "Great...";
		sageIntr5.thoughtsSpeed = 0.22f;
		ambientContainer.Add (sageIntr5);

		ambientInst sageIntr6 = new ambientInst ();
		sageIntr6.id = 5;
		sageIntr6.response = "'I can see you all have questions. I will take my time to answer them, don't you worry. But first, I'll need a drink. It's been a long journey.'";
		sageIntr6.thoughts = "The people seem to eagerly await his every move, but Deidre (the innkeep) instinctually sets into motion at the order of a drink. The crowd loosens up, and the murmur begins anew, as the guest waltzes through the inn.";
		ambientContainer.Add (sageIntr6);
		
		ambientInst sageIntr7 = new ambientInst ();
		sageIntr7.id = 6;
		sageIntr7.response = "";
		sageIntr7.thoughts = "The Sage sits down and immediately people start asking questions he answers in the most leasurely way possible.";
		ambientContainer.Add (sageIntr7);

		ambientInst exitSage = new ambientInst ();
		exitSage.id = 7;
		exitSage.disengage = true;
		exitSage.NextTrigger("shouldTalkToSage",true);
		ambientContainer.Add (exitSage);
		
		allAmbients.Add ("sageIntro", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region pondIntro
		ambientInst pondInt = new ambientInst ();
		pondInt.id = 0;
		pondInt.response = "<Ceara lets out a long sigh.>  ¤'Damn it was hot in there'    ¤'Yeah' <I say.>";
		pondInt.ambDelay = 1f;
		pondInt.thoughts = "We trudge our way through the thick crowds and breathe fresh air again.";
		ambientContainer.Add (pondInt);

		ambientInst pondInt1 = new ambientInst ();
		pondInt1.id = 1;
		pondInt1.response = "So... about that pond, huh? <she asks.>";
		pondInt1.thoughtsDelay = 1f;
		pondInt1.thoughts = "I don’t have to think twice before answering that.";
		ambientContainer.Add (pondInt1);

		ambientInst pondInt2 = new ambientInst ();
		pondInt2.id = 2;
		pondInt2.response = "";
		pondInt2.thoughtsDelay = 0.5f;
		pondInt2.thoughts = "The pond is a bit towards Caudden, which is why we’re generally not allowed to go there. But it also has color-fish and wild moving-plants, and an ancient willowtree that’s great fun to climb.";
		ambientContainer.Add (pondInt2);

		ambientInst PondIntEx = new ambientInst ();
		PondIntEx.id = 3;
		PondIntEx.disengage = true;
		PondIntEx.NextTrigger("treeBet",true);
		ambientContainer.Add (PondIntEx);
		
		allAmbients.Add ("pondIntro", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region race
		ambientInst race = new ambientInst ();
		race.id = 0;
		race.response = "";
		race.thoughtsSpeed = 0.01f;
		race.thoughts = "The tree is at the other side of the pond. We start sprinting to the left of the shallow water, and I faintly notice movement under the surface.";
		ambientContainer.Add (race);

		ambientInst race1 = new ambientInst ();
		race1.id = 1;
		race1.response = "";
		race1.thoughtsSpeed = 0.008f;
		race1.thoughts = "My feet feel light and springy on the summer grass, the smell of the waving flowers seething into my nostrils. We’re sidestepping toe to toe, whistling across the pond-side almost soundlessly, as we hear the wind clatter at our ears.";
		ambientContainer.Add (race1);

		ambientInst race2 = new ambientInst ();
		race2.id = 2;
		race2.response = "";
		race2.thoughtsSpeed = 0.007f;
		race2.thoughts = "She had a slight head start, so while I would consider myself faster, she gets her first foot up on the tree. Oddly coloured flowers pique my interest for a thin moment, but I pull myself up, clawing my bare feet into the slitful bark. I can feel my feet pinch a little, almost feel pain, but I pull myself up and latch onto a branch on top of me.";
		ambientContainer.Add (race2);

		ambientInst race3 = new ambientInst ();
		race3.id = 3;
		race3.response = "";
		race3.thoughtsSpeed = 0.007f;
		race3.thoughts = "We hear the cracking of branches and the rough sliding of skin across tree-bark as we both scramble up the unruly tree, mowing away any little annoyance in our way, were it branches or leaves or each other. My feet skid and tumble, desperate to grip anything and clutching down at it when I do.";
		ambientContainer.Add (race3);

		ambientInst race4 = new ambientInst ();
		race4.id = 4;
		race4.response = "";
		race4.thoughtsSpeed = 0.007f;
		race4.thoughts = "Ceara’s got her hand above me and I step up, but she sees me coming and slides her hand up, blocking my fingers from reaching the tree. With a smile she steps up and hurls herself further upward while I have to reset my grip, almost falling over in sheer surprise.";
		ambientContainer.Add (race4);
		
		ambientInst raceExit = new ambientInst ();
		raceExit.id = 5;
		raceExit.disengage = true;
		raceExit.NextTrigger("pullAmbient",false);
		ambientContainer.Add (raceExit);
		
		allAmbients.Add ("race", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region pullAmbient

		ambientInst pull = new ambientInst ();
		pull.id = 0;
		pull.response = "";
		pull.thoughts = "However, she smiled a little too long. As she reaches above me, her grip falters, and she keels sideways, trying to get a grip with her foot.";
		pull.thoughtsSpeed = 0.007f;
		ambientContainer.Add (pull);
		
		ambientInst pull1 = new ambientInst ();
		pull1.id = 1;
		pull1.response = "";
		pull1.thoughts = "She's in a bad spot, though, so her foot finds no solid branch to stay on, and without holding anything, her balance is tipped over.";
		pull1.thoughtsSpeed = 0.007f;
		ambientContainer.Add (pull1);
		
		ambientInst pull2 = new ambientInst ();
		pull2.id = 2;
		pull2.response = "";
		pull2.thoughts = "She falls down from the pine, glancing down with a shriek.";
		pull2.thoughtsSpeed = 0.007f;
		ambientContainer.Add (pull2);

		ambientInst pull4 = new ambientInst ();
		pull4.id = 3;
		pull4.response = "";
		pull4.thoughts = "I started climbing down to her after a short while. She wasn’t far away, and it hadn’t been a long fall, but she keeps lying there.     ¤‘Ceara?’ I said, looking down at her still body. I hadn’t judged the fall to be that hard.      ¤I slid down the tree as fast as I could, gliding with my feet over the rough surface, hearing it crunch under me.";
		ambientContainer.Add (pull4);

		ambientInst pull5 = new ambientInst ();
		pull5.id = 4;
		pull5.response = "<I approached her slowly.>   ¤'Are you okay?'";
		pull5.thoughts = "";
		ambientContainer.Add (pull5);

		ambientInst pullEx = new ambientInst ();
		pullEx.id = 5;
		pullEx.disengage = true;
		pullEx.NextTrigger("cCursed",true);
		ambientContainer.Add (pullEx);
		
		allAmbients.Add ("pullAmbient", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region cursedGoBackToInn
		ambientInst cursedGoBackToInn = new ambientInst ();
		cursedGoBackToInn.id = 0;
		cursedGoBackToInn.response = "";
		cursedGoBackToInn.thoughts = "As she begins moving I get a glance around us. It is all around the pond, in small, hollow patches of purple, as if the rain had left purple marks where it landed. I didn't even notice it as strange before.";
		ambientContainer.Add (cursedGoBackToInn);

		ambientInst cursedGoBackToInn1 = new ambientInst ();
		cursedGoBackToInn1.id = 1;
		cursedGoBackToInn1.response = "";
		cursedGoBackToInn1.thoughts = "We walk back slowly. She stumbles every few steps, walking slowly and carefully, feeling her body as if it was dangerous to touch.      ¤I ask her several more times if she is okay, but she keeps saying nothing. ¤I don't know what else to ask.";
		ambientContainer.Add (cursedGoBackToInn1);

		ambientInst cursedGoBackToInn2 = new ambientInst ();
		cursedGoBackToInn2.id = 2;
		cursedGoBackToInn2.response = "";
		cursedGoBackToInn2.thoughts = "The clouds are moving crazily above us. They twirl and spin with an odd quality about them; they are too thin, or too fickle.";
		ambientContainer.Add (cursedGoBackToInn2);

		ambientInst cursedGoBackToInn3 = new ambientInst ();
		cursedGoBackToInn3.id = 3;
		cursedGoBackToInn3.response = "";
		cursedGoBackToInn3.thoughts = "When we come back to the inn, nothing has changed. The entire village is still listening to the words of a stranger. Ceara waits for me to open the door, afraid to interact with anything.";
		ambientContainer.Add (cursedGoBackToInn3);

		ambientInst cursedGoBackToInn4 = new ambientInst ();
		cursedGoBackToInn4.id = 4;
		cursedGoBackToInn4.response = "Help! <I shout when we get in.> She needs help! Ceara's got something!";
		cursedGoBackToInn4.thoughts = "At the mention of Ceara’s name people start paying attention.         ¤The first to come running was Ceara’s mom, Mauge, and the rest give us a berth as we enter, quickly seeing the oddness of Ceara’s skin.";
		cursedGoBackToInn4.thoughtsDelay = 1.5f;
		ambientContainer.Add (cursedGoBackToInn4);

		ambientInst cursedGoBackToInn5 = new ambientInst ();
		cursedGoBackToInn5.id = 5;
		cursedGoBackToInn5.response = "'Are you alright?' <she demands as she gets close to embrace her daughter.> ¤'DON’T' <Ceara yells and fling herself backwards, leaving a mother with the most startled expression I have ever seen.>";
		cursedGoBackToInn5.thoughts = "The rest of the inn is now watching with wide, horrified eyes at the girl with skin that looks... wrong.";
		cursedGoBackToInn5.thoughtsDelay = 4f;
		ambientContainer.Add (cursedGoBackToInn5);
		
		ambientInst cursedGoBackToInnExit = new ambientInst ();
		cursedGoBackToInnExit.id = 6;
		cursedGoBackToInnExit.disengage = true;
		cursedGoBackToInnExit.NextTrigger("innCurseIntro",true);
		ambientContainer.Add (cursedGoBackToInnExit);
		
		allAmbients.Add ("cursedGoBackToInn", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region outsideInn
		List<ambientInst> outsideInnList = new List<ambientInst> ();
		outsideInnList.Add(new ambientInst());
		outsideInnList [0].id = 0;
		outsideInnList [0].response = "";
		outsideInnList [0].thoughts = "I lead them outside, towards the pond, round the corner and down a pathway, almost half the village in tow when I look at the grass.";
		ambientContainer.Add (outsideInnList [0]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [1].id = 1;
		outsideInnList [1].response = "";
		outsideInnList [1].thoughts = "It is spreading up from the pond. Moving with the flowers. I hadn’t noticed it before, so it must have been moving fast. Really fast. I stare, horrified at the new color the tiny pathway has gotten, once leading down to the pond with tufts of yellow flowers along downtrodden grass.";
		ambientContainer.Add (outsideInnList [1]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [2].id = 2;
		outsideInnList [2].response = "'What the hell...'      ¤'Was this here before?' <the Sage asks, getting a worried expression.>     ¤'No' <I say.>";
		outsideInnList [2].thoughts = "Now, it is covered in purple-green stripes, splashed in uneven patterns that makes everything worse.";
		outsideInnList [2].ambDelay = 1.5f;
		ambientContainer.Add (outsideInnList [2]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [3].id = 3;
		outsideInnList [3].response = "'Shit. That’s not good.'";
		outsideInnList [3].thoughts = "I don’t know what to do.";
		outsideInnList [3].thoughtsDelay = 0.5f;
		ambientContainer.Add (outsideInnList [3]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [4].id = 4;
		outsideInnList [4].response = "'What is it? Can you fix it?' <Mauge asks.>";
		outsideInnList [4].thoughts = "He doesn’t respond for some time, all of us staring at him in incredulity, afraid to go forward and unwilling to go back.";
		outsideInnList [4].thoughtsDelay = 1.0f;
		ambientContainer.Add (outsideInnList [4]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [5].id = 5;
		outsideInnList [5].response = "'It looks like an infestation, a spread of a disease. Never seen something like this spread this fast, though. Nor travel from plants to humans.'   ¤<He pauses again.>      ¤'Listen, I’m going to need some equipment, then I can investigate its nature. Unfortunately, I might have to go to Caudden to get help.'";
		outsideInnList [5].thoughts = "";
		outsideInnList [5].thoughtsDelay = 1.0f;
		ambientContainer.Add (outsideInnList [5]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [6].id = 6;
		outsideInnList [6].response = "'It doesn’t look like we have that kind of time if it spreads that fast... sir'";
		outsideInnList [6].thoughts = "It is hard to see the actual movement, but it now touches some plants it hadn't before. I’m sure of it.";
		ambientContainer.Add (outsideInnList [6]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [7].id = 7;
		outsideInnList [7].response = "'What do you need?' <Ceara asks.> ¤'A compass, fire, bones of a dead animal, preferably a mouse or cat, something made of iron, a casket or pan or something, and a sample of a flower with the infection on it.'";
		outsideInnList [7].thoughts = "We looked around at each other, considering the list of ingredients.";
		outsideInnList [7].thoughtsDelay = 3f;
		ambientContainer.Add (outsideInnList [7]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [8].id = 8;
		outsideInnList [8].response = "'I can get the flower' <Ceara says.> 'I’m already touched, right? What does it matter if I touch it again?'  ¤<The Sage nods.> 'Still, I would get some gloves.'  ¤<She nods and starts moving.>";
		outsideInnList [8].thoughts = "I'm surprised at how calm Ceara is taking this. There is almost no hesitation, no fear in her voice.";
		outsideInnList [8].thoughtsDelay = 3f;
		ambientContainer.Add (outsideInnList [8]);

		outsideInnList.Add(new ambientInst());
		outsideInnList [9].id = 9;
		outsideInnList [9].response = "<Mauge begins looking at the others.> 'We need a fire! Anyone’s got a dead animal perchance? Or something we can slaughter?'";
		outsideInnList [9].thoughts = "Several voices rise up, start yelling at each other in a frantic language I can only understand if I pay close attention to one of the sounds—the rest are drowning out as noise.";
		outsideInnList [9].thoughtsDelay = 1f;
		ambientContainer.Add (outsideInnList [9]);

		outsideInnList.Add (new ambientInst ());
		outsideInnList [10].id = 10;
		outsideInnList [10].disengage = true;
		//outsideInnList [10].NextTrigger("befRitualChoice",true);
		outsideInnList [10].NextTrigger("ritTrans",false);
		ambientContainer.Add (outsideInnList[10]);
		
		allAmbients.Add ("outsideInn", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region cearaRitIntro
		ambientInst cearaRitIntro = new ambientInst ();
		cearaRitIntro.id = 0;
		cearaRitIntro.response = "";
		cearaRitIntro.thoughts = "We return with the plant to see the rest of the village in disarray. Some are by the roadside, building a small fire, many are standing around in various clumps not doing anything, and two people are crying. The Sage isn’t there.";
		ambientContainer.Add (cearaRitIntro);

		ambientInst cearaRitIntro1 = new ambientInst ();
		cearaRitIntro1.id = 1;
		cearaRitIntro1.response = "'Ceara!' <Mauge says, holding a pan and rushing to her daughter> 'Did you get the plant?'   ¤'Yeah. What’s happening? Did you finish?'";
		cearaRitIntro1.thoughts = "The looks of the others says enough. Some glance up, shake their head, rustle their feet, cross their arms, but no one comes to help. No one comes to ask her if she is okay. Mikal’s wife Perna, who is crying, is watching some of the others hold a dead cat, throat slit.     ¤Oh.";
		ambientContainer.Add (cearaRitIntro1);

		ambientInst cearaRitIntro3 = new ambientInst ();
		cearaRitIntro3.id = 2;
		cearaRitIntro3.response = "Do we have everything? <Ceara asks. She sounds worried. There's something crass about her voice.>  ¤Yeah, we got everything. <Ceara looks back at Perna> 'You had to—'¤'Yes! We had to. Look at it.' <She points towards the pond. There is no reason to go down to see the purple anymore. It is so close it will hit the first houses soon enough.>";
		cearaRitIntro3.thoughts = "I look at Ceara. She seems worried. There is something she isn’t telling us.";
		ambientContainer.Add (cearaRitIntro3);

		ambientInst cearaRitIntroExit = new ambientInst ();
		cearaRitIntroExit.id = 3;
		cearaRitIntroExit.disengage = true;
		cearaRitIntroExit.NextTrigger("ritual",false);
		ambientContainer.Add (cearaRitIntroExit);
		
		allAmbients.Add ("cearaRitIntro", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region stayRitIntro
		ambientInst stayRitIntrow = new ambientInst ();
		stayRitIntrow.id = 0;
		stayRitIntrow.response = "";
		stayRitIntrow.thoughts = "The atmosphere is weird when they leave. The Sage wasn’t back either. Ceara is gone, Mauge is gone. Perna is gone. Tripping, I'm anxious, staring back the way Ceara left, wondering if I should have gone after her. People glare with trepidation to the south, at the purple mess that is inching closer. Maybe some of them are considering if they should run. I wouldn’t blame them.";
		ambientContainer.Add (stayRitIntrow);

		ambientInst stayRitIntrow1 = new ambientInst ();
		stayRitIntrow1.id = 1;
		stayRitIntrow1.response = "";
		stayRitIntrow1.thoughts = "Illij comes back with a compass and holds it while he looks around, waiting with the rest of us.";
		ambientContainer.Add (stayRitIntrow1);

		ambientInst stayRitIntrow2 = new ambientInst ();
		stayRitIntrow2.id = 2;
		stayRitIntrow2.response = "";
		stayRitIntrow2.thoughts = "Perna comes with with a dead cat, crying, with Mikal supporting her as they walk. Damn, that hurts more than I expected to. They slit its throat, clean, but with blood running down Perna’s arms.      ¤¤Shit...";
		ambientContainer.Add (stayRitIntrow2);

		ambientInst stayRitIntro = new ambientInst ();
		stayRitIntro.id = 3;
		stayRitIntro.response = "'Hey Tari.'         ¤'Are you okay?' <I settle with.>";
		stayRitIntro.thoughts = "Ceara returns with a plant in her hand shortly after. I go over to her and think of fifteen things to say but none of them fits.";
		stayRitIntro.ambDelay = 3f;
		ambientContainer.Add (stayRitIntro);

		ambientInst stayRitIntro1 = new ambientInst ();
		stayRitIntro1.id = 4;
		stayRitIntro1.response = "Ceara! <Mauge says before I get an answer, pan in hand> Did you get the plant?  ¤Yeah. What’s happening? Did you get the rest?";
		stayRitIntro1.thoughts = "Ceara spots Perna and Mikal and her face becomes stern, but she doesn’t say anything.";
		ambientContainer.Add (stayRitIntro1);

		ambientInst stayRitIntro2 = new ambientInst ();
		stayRitIntro2.id = 5;
		stayRitIntro2.response = "Do we have everything? <She repeats.>  ¤Yeah, we got everything. ¤<Ceara looks at Perna> 'You had to—'¤Yes! We had to. Look at it.' <She points towards the pond. There is no reason to go down to see the purple anymore. It is close enough that it will hit the first houses soon enough.>";
		stayRitIntro2.thoughts = "";
		ambientContainer.Add (stayRitIntro2);

		ambientInst stayRitIntroEx = new ambientInst ();
		stayRitIntroEx.id = 6;
		stayRitIntroEx.disengage = true;
		stayRitIntroEx.NextTrigger("ritual",false);
		ambientContainer.Add (stayRitIntroEx);
		
		allAmbients.Add ("stayRitIntro", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion


		#region ritualTransition

		ambientInst ritTrans = new ambientInst ();
		ritTrans.id = 0;
		ritTrans.response = "";
		ritTrans.thoughts = "People move, the Sage goes back to get some things, he doesn't say what. The rest of us stand around, gathering the things he asked for, tense and without talking much. The biggest problem is the cat, but Perna's is old, so she gives it up for slaughter.";
		ambientContainer.Add (ritTrans);

		ambientInst ritTrans1 = new ambientInst ();
		ritTrans1.id = 1;
		ritTrans1.response = "";
		ritTrans1.thoughts = "Ceara returns with a plant, looking more distant than before. I try talking to her but she doesn't say much. Mauge tries as well but she keeps saying she's fine.";
		ambientContainer.Add (ritTrans1);

		ambientInst ritTrans2 = new ambientInst ();
		ritTrans2.id = 2;
		ritTrans2.response = "";
		ritTrans2.thoughts = "When we're ready, we wait for the Sage to come back.";
		ambientContainer.Add (ritTrans2);


		ambientInst ritTransEx = new ambientInst ();
		ritTransEx.id = 3;
		ritTransEx.disengage = true;
		ritTransEx.NextTrigger("ritual",false);
		ambientContainer.Add (ritTransEx);

		allAmbients.Add ("ritTrans", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion


		#region ritual
		ambientInst rit = new ambientInst ();
		rit.id = 0;
		rit.response = "";
		rit.thoughts = " We look at each other, waiting for the world to envelop us. Waiting for things to make sense. Waiting for us all to wake up.";
		ambientContainer.Add (rit);

		ambientInst rit1 = new ambientInst ();
		rit1.id = 1;
		rit1.response = "Is everything ready? Come here with the plant <he says> ";
		rit1.thoughts = "We hear rustling footsteps coming from the inn again. It’s the Sage, looking at us with morbid eyes. ";
		rit1.ambDelay = 2f;
		ambientContainer.Add (rit1);

		ambientInst rit2 = new ambientInst ();
		rit2.id = 2;
		rit2.response = "";
		rit2.thoughts = "Perna hands over the cat, Illij a compass, and Mauge an iron pan. The Sage nods to himself as he sees the items, and bends down by the fire.";
		ambientContainer.Add (rit2);

		ambientInst rit3 = new ambientInst ();
		rit3.id = 3;
		rit3.response = "";
		rit3.thoughts = "It takes a long time before anything happens. He works in the middle of the road, where we slapdashed a fire together, and the rest of us stand in small circles, looking at the strange clouds and the purple get closer and closer. None of us panic. We don’t know if we need to.";
		ambientContainer.Add (rit3);

		ambientInst rit4 = new ambientInst ();
		rit4.id = 4;
		rit4.response = "";
		rit4.thoughts = "I look over to Ceara, but before I say anything I notice how she’s standing. One leg limp and not using any force, the other completely straight and leaning against the wall. None of her hands touch the wall. She’s just leaning with her shoulder, her arms a dead limp.";
		ambientContainer.Add (rit4);

		ambientInst rit5 = new ambientInst ();
		rit5.id = 5;
		rit5.response = "'Shit, are you okay?' <I ask.>    ¤<She doesn’t respond.> ¤'Gods, are you... Ceara!'";
		rit5.thoughtsDelay = 2f;
		rit5.thoughts = "I lean in close to see her face, but she’s... Her eyes are sheets of white, her face more covered than the last time than the last time I saw it.";
		ambientContainer.Add (rit5);

		ambientInst rit6 = new ambientInst ();
		rit6.id = 6;
		rit6.response = "'Mauge!' <I scream, but then I hear muffled sounds.>   ¤'No, no... it’s... I’m...' <Ceara says, but she never finishes that sentence before Mauge gets here.> ¤'Oh, gods' <she says and raises her hands to her mouth.> 'Sage! Please! She’s gotten worse!'";
		rit6.thoughts = "Worse doesn’t even describe it. Mauge cracks into tears as she speaks, Ceara barely answers.";
		rit6.thoughtsDelay = 5f;
		ambientContainer.Add (rit6);

		ambientInst rit7 = new ambientInst ();
		rit7.id = 7;
		rit7.response = "<The Sage looks at her and furrows his brow.>   ¤'Can she get over here?'   ¤'Can you walk?' <Mauge and I both say, and Ceara starts shuffling along.>";
		rit7.thoughtsDelay = 3f;
		rit7.thoughts = "I instinctually lean in to help her but remember myself and stop.";
		ambientContainer.Add (rit7);

		ambientInst rit8 = new ambientInst ();
		rit8.id = 8;
		rit8.response = "";
		rit8.thoughts = "Ceara scrapes herself by, the entire village watching in fear as they realize what happened to her—how real it is. Most of them glance down south one extra time, whispering to each other. I glance down there too, checking my legs and arms one extra time for any purple marks.";
		ambientContainer.Add (rit8);

		ambientInst rit9 = new ambientInst ();
		rit9.id = 9;
		rit9.response = "'How are you? Is there a fever? Do you feel numb? Like you’re about to fall asleep?'  ¤<Ceara doesn’t even have time to answer all the questions. And the Sage doesn’t wait either.>";
		rit9.ambDelay = 3f;
		rit9.thoughts = "She collapses in front of the Sage, right beside his fire and he immediately uses a small duster to roll the plant beside him.";
		ambientContainer.Add (rit9);

		ambientInst rit10 = new ambientInst ();
		rit10.id = 10;
		rit10.response = "";
		rit10.thoughts = "He takes the bones and lays them in the pan, then begins grinding them as if they're a pestle and the pan's a mortar. He lays the compass beside, seeing it bend nicely towards the pan as he does. When most of the pan is covered in a layer of bone dust, he picks up the plants and grinds that in with the rest. While he sets the pan on the fire he pulls some small yellow flakes from his pocket, and pours these on as well.";
		ambientContainer.Add (rit10);

		ambientInst rit11 = new ambientInst ();
		rit11.id = 11;
		rit11.response = "'It’s...' <the Sage mumbles.> 'But? No...' <he pauses again.>";
		rit11.thoughtsDelay = 1.5f;
		rit11.thoughts = "The entire village is watching in tension. The wind is blowing heavily, more heavily than it did a minute ago.";
		ambientContainer.Add (rit11);

		ambientInst rit12 = new ambientInst ();
		rit12.id = 12;
		rit12.response = "'Shit' <he says.>";
		rit12.thoughts = "That’s all. That’s all he says for a long time.    ¤The compass arrow is running amok, pointing everywhere every second it can.";
		rit12.thoughtsDelay = 0.8f;
		ambientContainer.Add (rit12);

		ambientInst rit13 = new ambientInst ();
		rit13.id = 13;
		rit13.response = "'What is it? Can you cure her?'   ¤<The Sage looks up, as if from a long slumber.> 'Only tend her. This is something I have never seen before. They might help us in Caudden, if it’s not there already. Whatever it is, it’s bad. Really bad.'";
		rit13.thoughts = "";
		ambientContainer.Add (rit13);

		ambientInst rit14 = new ambientInst ();
		rit14.id = 14;
		rit14.response = "...";
		rit14.thoughtsDelay = 0.8f;
		rit14.thoughts = "It takes a long time before anyone does anything. It feels like an hour. It looks like an eternity.                ¤¤Then it’s as if everyone starts shouting at once.";
		ambientContainer.Add (rit14);

		ambientInst rit15 = new ambientInst ();
		rit15.id = 15;
		rit15.response = "'What do we do?' ¤'Do we have to run? Where can we go?' ¤'Is it over? What about Ceara?' ¤'I can’t leave here! It’s my home.'";
		rit15.thoughtsSpeed = 0.004f;
		rit15.responseSpeed = 0.005f;
		rit15.thoughts = "Ceara looks terrible. She lies down beside the fire, as if with a fever, but all stricken across her face in that purple, dreadful color. The streaks of green are starting to show more clearly.    ¤Panic takes over. The Sage barely has time to respond to anything. The infestation or whatever the fuck it is, is really, really close now.";
		ambientContainer.Add (rit15);

		ambientInst rit16 = new ambientInst ();
		rit16.id = 16;
		rit16.response = "'We need to get to Caudden' <the Sage says> 'there they might have a cure.'  ¤'Caudden?!' <Mauge says> 'They’ll never take us in in Caudden with her looking like that! And what if it’s in there too, like you said?'  ¤'We have to take that chance.'";
		rit16.thoughts = "I feel a rumbling in my stomach, some low, crunching feeling that something’s wrong. Of course it is, I think, our entire village is about to be consumed by some weird purple shit that’s killing Ceara, but there’s something else too.";
		rit16.thoughtsSpeed = 0.004f;
		rit16.responseSpeed = 0.005f;
		ambientContainer.Add (rit16);

		ambientInst rit17 = new ambientInst ();
		rit17.id = 17;
		rit17.response = "'Nevermind' <Mauge says> 'we’re bloody leaving now. Come on, Ceara, let’s get out of here.'  ¤<She rushes towards Ceara and bends down to pick her up> 'No, don’t!' <the Sage says, but Mauge isn’t listening>.";
		rit17.thoughtsDelay = 1.5f;
		rit17.thoughtsSpeed = 0.004f;
		rit17.responseSpeed = 0.005f;
		rit17.thoughts = "Mauge’s skin turns purple immediately, but she picks her child up and carries her. She starts crying as soon as she steps away from the fire, looking down at her daughter. ";
		ambientContainer.Add (rit17);

		ambientInst rit18 = new ambientInst ();
		rit18.id = 18;
		rit18.response = "'Her skin is... soft' <she says.>";
		rit18.thoughtsSpeed = 0.004f;
		rit18.thoughts = "People are running now. In all directions, home to their houses, shutting themselves in or running to the stables to grab a horse, or just running north, into the forest.";
		ambientContainer.Add (rit18);

		ambientInst rit19 = new ambientInst ();
		rit19.id = 19;
		rit19.response = "";
		rit19.thoughtsSpeed = 0.01f;
		rit19.responseSpeed = 0.01f;
		rit19.thoughts = "Mauge tries to run as well, but with Ceara in her arms, she can only stumble along, her body already giving way to the stuff, as she clearly can’t rest on her left leg either. I almost begin to run as well, but stop.";
		ambientContainer.Add (rit19);

		ambientInst rit20 = new ambientInst ();
		rit20.id = 20;
		rit20.response = "";
		rit20.thoughts = "There it is. I notice it. The clouds aren’t just moving weird. They aren’t just picking up speed and circling unnaturally. They’re... close. And they’re tiny, so they look like they are as far away as usual. The entire village is enveloped in it, not really noticeable since it isn’t fog.";
		ambientContainer.Add (rit20);

		ambientInst ritEx = new ambientInst ();
		ritEx.id = 21;
		ritEx.disengage = true;
		ritEx.NextTrigger("ritChoice",true);
		ambientContainer.Add (ritEx);
		
		allAmbients.Add ("ritual", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion


		#region goesToCaudden
		ambientInst goesToCaudden = new ambientInst ();
		goesToCaudden.id = 0;
		goesToCaudden.response = "";
		goesToCaudden.thoughts = "I realize the infestation has caught up to us. It’s right there, on the grass beneath us, only about two feet away. The Sage rushes to the right, and I go with him, running now, to get away from a thing we can’t see moving—only taking leaps to where we are.";
		ambientContainer.Add (goesToCaudden);

		ambientInst goesToCaudden1 = new ambientInst ();
		goesToCaudden1.id = 1;
		goesToCaudden1.response = "";
		goesToCaudden1.thoughts = "We run towards the stables, where he says he has a horse.                 ¤ But I doubt it’s still there anymore.";
		ambientContainer.Add (goesToCaudden1);

		ambientInst goesToCauddenEx = new ambientInst ();
		goesToCauddenEx.id = 2;
		goesToCauddenEx.disengage = true;
		ambientContainer.Add (goesToCauddenEx);
		
		allAmbients.Add ("goesToCaudden", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

		#region goesNorth
		ambientInst goesNorth = new ambientInst ();
		goesNorth.id = 0;
		goesNorth.response = "";
		goesNorth.thoughts = "I realize the infestation has caught up to us. It’s right there, on the grass beneath us, only about two feet away. The Sage rushes to the right, and I run north, to get away from a thing we can’t see moving—only taking leaps to where we are.";
		ambientContainer.Add (goesNorth);
		
		ambientInst goesNorth1 = new ambientInst ();
		goesNorth1.id = 1;
		goesNorth1.response = "";
		goesNorth1.thoughts = "I don’t see where he goes but I just continue to run away, running away from the sun and everything I know.";
		ambientContainer.Add (goesNorth1);
		
		ambientInst goesNorthEx = new ambientInst ();
		goesNorthEx.id = 2;
		goesNorthEx.disengage = true;
		ambientContainer.Add (goesNorthEx);
		
		allAmbients.Add ("goesNorth", ambientContainer);
		ambientContainer = new List<ambientInst> ();
		#endregion

	}

}
