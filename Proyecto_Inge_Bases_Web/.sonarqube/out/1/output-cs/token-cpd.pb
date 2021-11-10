ß
bC:\Users\royam\source\repos\roymm\Subastas\Proyecto_Inge_Bases_Web\ServicioWeb\ProjectInstaller.cs
	namespace		 	
ServicioWeb		
 
{

 
[ 
RunInstaller 
( 
true 
) 
] 
public 

partial 
class 
ProjectInstaller )
:* +
System, 2
.2 3
Configuration3 @
.@ A
InstallA H
.H I
	InstallerI R
{ 
public 
ProjectInstaller 
(  
)  !
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} ¿>
ZC:\Users\royam\source\repos\roymm\Subastas\Proyecto_Inge_Bases_Web\ServicioWeb\Service1.cs
	namespace 	
ServicioWeb
 
{ 
public 

partial 
class 
Service1 !
:" #
ServiceBase$ /
{ 
Thread 
mainThreadObj 
; 
DateTime 
currentDateTime  
;  !
Thread 
objProcessThread 
;  
string 
recievermailid 
= 
$str  5
;5 6
string 
subject 
= 
$str .
;. /
string 
bodyText 
= 
$str ;
;; <
public 
Service1 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
public"" 
void"" 
start"" 
("" 
)"" 
{## 	
string%% 
[%% 
]%% 
args%% 
=%% 
default%% #
(%%# $
string%%$ *
[%%* +
]%%+ ,
)%%, -
;%%- .
OnStart&& 
(&& 
args&& 
)&& 
;&& 
}'' 	
	protected)) 
override)) 
void)) 
OnStart))  '
())' (
string))( .
[)). /
]))/ 0
args))1 5
)))5 6
{** 	
currentDateTime++ 
=++ 
DateTime++ &
.++& '
Now++' *
;++* +
mainThreadObj-- 
=-- 
new-- 
Thread--  &
(--& '
ProcessThread--' 4
)--4 5
;--5 6
mainThreadObj// 
.// 
Start// 
(//  
)//  !
;//! "
}11 	
	protected33 
override33 
void33 
OnStop33  &
(33& '
)33' (
{44 	
}55 	
private77 
void77 
ProcessThread77 "
(77" #
)77# $
{88 	
if99 
(99 
objProcessThread99  
==99! #
null99$ (
||99) +
objProcessThread:: 
.:: 
ThreadState:: (
==::) +
System::, 2
.::2 3
	Threading::3 <
.::< =
ThreadState::= H
.::H I
Stopped::I P
||::Q S
objProcessThread;; 
.;; 
ThreadState;; (
==;;) +
System;;, 2
.;;2 3
	Threading;;3 <
.;;< =
ThreadState;;= H
.;;H I
	Unstarted;;I R
);;R S
{<< 
while?? 
(?? 
DateTime?? 
.??  
Now??  #
>=??$ &
currentDateTime??' 6
.??6 7

AddMinutes??7 A
(??A B
$num??B C
)??C D
)??D E
{@@ 
objProcessThreadAA $
=AA% &
newAA' *
ThreadAA+ 1
(AA1 2
threadProcessMethodAA2 E
)AAE F
;AAF G
objProcessThreadCC $
.CC$ %
IsBackgroundCC% 1
=CC2 3
trueCC4 8
;CC8 9
objProcessThreadEE $
.EE$ %
StartEE% *
(EE* +
)EE+ ,
;EE, -
currentDateTimeGG #
=GG$ %
currentDateTimeGG& 5
.GG5 6

AddSecondsGG6 @
(GG@ A
$numGGA B
)GGB C
;GGC D
ThreadII 
.II 
SleepII  
(II  !
$numII! '
)II' (
;II( )
ProcessThreadKK !
(KK! "
)KK" #
;KK# $
}LL 
ThreadNN 
.NN 
SleepNN 
(NN 
$numNN #
)NN# $
;NN$ %
ProcessThreadPP 
(PP 
)PP 
;PP  
}QQ 
}RR 	
privateTT 
voidTT 
threadProcessMethodTT (
(TT( )
)TT) *
{UU 	
sendmailVV 
(VV 
recievermailidVV #
,VV# $
subjectVV% ,
,VV, -
bodyTextVV. 6
)VV6 7
;VV7 8
}WW 	
publicXX 
boolXX 
sendmailXX 
(XX 
stringXX #
recievermailidXX$ 2
,XX2 3
stringXX4 :
subjectXX; B
,XXB C
stringXXD J
bodyTextXXK S
)XXS T
{YY 	
tryZZ 
{[[ 
string\\ 
senderId\\ 
=\\  !
$str\\" =
;\\= >
string]] 
senderPassword]] %
=]]& '
$str]]( 4
;]]4 5
System__ 
.__ 
Net__ 
.__ 
Mail__ 
.__  
MailMessage__  +
mailMessage__, 7
=__8 9
new__: =
System__> D
.__D E
Net__E H
.__H I
Mail__I M
.__M N
MailMessage__N Y
(__Y Z
)__Z [
;__[ \
mailMessage`` 
.`` 
To`` 
.`` 
Add`` "
(``" #
recievermailid``# 1
)``1 2
;``2 3
mailMessageaa 
.aa 
Fromaa  
=aa! "
newaa# &
MailAddressaa' 2
(aa2 3
senderIdaa3 ;
)aa; <
;aa< =
mailMessagecc 
.cc 
Subjectcc #
=cc$ %
subjectcc& -
;cc- .
mailMessagedd 
.dd 
SubjectEncodingdd +
=dd, -
Systemdd. 4
.dd4 5
Textdd5 9
.dd9 :
Encodingdd: B
.ddB C
UTF8ddC G
;ddG H
mailMessageff 
.ff 
Bodyff  
=ff! "
bodyTextff# +
;ff+ ,
mailMessagegg 
.gg 
BodyEncodinggg (
=gg) *
Systemgg+ 1
.gg1 2
Textgg2 6
.gg6 7
Encodinggg7 ?
.gg? @
UTF8gg@ D
;ggD E
mailMessagehh 
.hh 

IsBodyHtmlhh &
=hh' (
falsehh) .
;hh. /
mailMessagejj 
.jj 
Priorityjj $
=jj% &
MailPriorityjj' 3
.jj3 4
Highjj4 8
;jj8 9

SmtpClientll 

smtpClientll %
=ll& '
newll( +

SmtpClientll, 6
(ll6 7
)ll7 8
;ll8 9

smtpClientmm 
.mm 
Credentialsmm &
=mm' (
newmm) ,
Systemmm- 3
.mm3 4
Netmm4 7
.mm7 8
NetworkCredentialmm8 I
(mmI J
senderIdmmJ R
,mmR S
senderPasswordmmT b
)mmb c
;mmc d

smtpClientnn 
.nn 
Portnn 
=nn  !
$numnn" %
;nn% &

smtpClientoo 
.oo 
Hostoo 
=oo  !
$stroo" 2
;oo2 3

smtpClientpp 
.pp 
	EnableSslpp $
=pp% &
truepp' +
;pp+ ,
objectrr 
	userStaterr  
=rr! "
mailMessagerr# .
;rr. /
trytt 
{uu 

smtpClientvv 
.vv 
Sendvv #
(vv# $
mailMessagevv$ /
)vv/ 0
;vv0 1
returnww 
trueww 
;ww  
}xx 
catchyy 
(yy 
Systemyy 
.yy 
Netyy !
.yy! "
Mailyy" &
.yy& '
SmtpExceptionyy' 4
)yy4 5
{zz 
return{{ 
false{{  
;{{  !
}|| 
}}} 
catch~~ 
(~~ 
	Exception~~ 
)~~ 
{ 
return
€€ 
false
€€ 
;
€€ 
}
 
}
‚‚ 	
}
ƒƒ 
}„„ ·
YC:\Users\royam\source\repos\roymm\Subastas\Proyecto_Inge_Bases_Web\ServicioWeb\Program.cs
	namespace

 	
ServicioWeb


 
{ 
static 	
class
 
Program 
{ 
static 
void 
Main 
( 
) 
{ 	
ServiceBase 
[ 
] 
ServicesToRun '
;' (
ServicesToRun 
= 
new 
ServiceBase  +
[+ ,
], -
{ 
new 
Service1 
( 
) 
} 
; 
ServiceBase 
. 
Run 
( 
ServicesToRun )
)) *
;* +
} 	
} 
} ”
iC:\Users\royam\source\repos\roymm\Subastas\Proyecto_Inge_Bases_Web\ServicioWeb\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str &
)& '
]' (
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str (
)( )
]) *
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *