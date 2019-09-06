%%Ambient%%
There's already a crowd gathering in the inn.  
Nearly half the village is here, talking loudly about wild rumours, misinforming, and just generally enjoying themselves.
§§"Come to see the show?" >he asks.< #D=2.5
* [Next]
Me and Ceara scout Illij sitting by a corner table alone as he usually does. 
We sit down next to him before he even notices us.
* * [Next]
-> IllijTalk

=== IllijTalk ===
%%Dialogue%%
"That's our plan" >says Ceara.   <
"Well, you"re in for something at least. This guy's the real deal."
§§He seems more excited than usual.
-> Questions


=== Questions //Ceara interrupts after third and fourth question. You will not get to ask all of them. You will miss one.
{!|||->nomorequestions||->mother}
* [Who is he?]
-> who
* [What's he doing here?]
-> what
* { who } [Ceara says you know him?]
-> know
* { who }  [We don't have any {TURNS_SINCE(-> who) < 1 :of that|demons} here?]
-> curses
* { what } [What did Mikal say? Did he meet him?]
-> mikal
* -> nomorequestions //fallback choice!


= who
"He's a Vraadii Sage."  
"Vraadii?" >Ceara asks.<
"Sages who deal with curses and demons."
-> Questions

= what
"He's probably heading to Caudden, but Mikal said he"d stay here a few days for some reason. Maybe he wants to see the city from out here. Who knows." 
-> Questions

= curses
"We don't? You never really know until he's had a look around the place. They tend to spot things like that as the first, too."
-> Questions


= know
"Yeah, maybe. If it's who I think."
-> Questions

= sage
"His name is Derec. He... rid my last village of a curse that had the cows eat bad food, producing terrible milk for several months. He whisked it away in seconds.
-> Questions

= mikal
"Says he did, yeah. Out by Furrow, on his way back from a Courier trip."
-> Questions

= nomorequestions
"Who's going to talk to him?" >Ceara asks.<
"Who isn't? Half the town's already here, waiting eagerly..."
-> Questions


= mother
"My mother's gonna speak to him?" >Ceara asks, again.<

"Says she's planning on it. You want to speak to him too, don't you?"            
"Well..." >Ceara is hesitating.< #D=0.3

* [Sure]
"Get in line, then. It's rare to see someone like that here" >Illij says.  <

"Most of them dart directly into Caudden and never look back" >Ceara says.<   

"Exactly. This one's staying, which makes him interesting."
* * [When's he coming?]
* [Maybe]
"Bah, you know you want to. No reason to hide it. I don't care that you act all tough. It's only natural to have questions" >Illij says.<
* * [When's he coming?]
* [Not really]
"Hah, you little liar. Don't try to act tough around me, I can see right through that. You got the same negativity, Ceara?"       

"I don't know" >she says.< "Maybe."         

>He shakes his head.< "Bunch of girls, you are."
* * [When's he coming?]

- (when)
>But I don't get to ask that question. Ceara and Illij both look away from me.<
§§...What's?
* [-Look over-]
@@Tari_InnSageArrives
-> END