(* ::Package:: *)

c=1


p=Map[{#[[1]]*Sin[#[[2]]]-1,#[[1]]*Cos[#[[2]]]-1}&,Table[{RandomReal[],RandomReal[2Pi]},8]]


n=Map[{#[[1]]*Sin[#[[2]]]+1,#[[1]]*Cos[#[[2]]]+1}&,Table[{RandomReal[],RandomReal[2Pi]},8]]


d=Join[Map[{#,1}&,p],Map[{#,-1}&,n]]


Dynamic[Show[ContourPlot[Dot[omega,{x,y}]+b==0,{x,-2,2},{y,-2,2}],ListPlot[{p,n}]]]


Dynamic[Alpha,Alpha=Table[0,Length[d]]]


Bound[l_,alpha_,h_]:=If[alpha>h,h,If[alpha<l,l,alpha]]


Dynamic[omega,omega=Table[0,2]]


Dynamic[b,b=0]


Dynamic[gamma,gamma={}]


Dynamic[ListPlot[gamma]]


F[x_]:=Dot[omega,x]+b


Dynamic[Error,Error=Map[F[#[[1]]]-#[[2]]&,d]]


UpdateSVM[i_,j_]:=Module[{xi,xj,yi,yj,alphai,alphaj,alphainew,alphajnew,inf,sup,bi,bj,si},
If[i==j,Return[]];
{xi,yi}=d[[i]];{xj,yj}=d[[j]];
alphai=Alpha[[i]];alphaj=Alpha[[j]];
inf=If[yi==yj,Max[0,alphai+alphaj-c],Max[0,alphai-alphaj]];
sup=If[yi==yj,Min[c,alphai+alphaj],Min[c,alphai-alphaj+c]];
If[inf==sup,Return[]];
alphainew=Bound[inf,alphai+yi*(Error[[j]]-Error[[i]])/Norm[xi-xj]^2,sup];
alphajnew=alphaj+(alphai-alphainew)*yi*yj;
bi=b-Error[[i]]+(alphai-alphainew)*yi*Dot[xi,xi]+(alphaj-alphajnew)*yj*Dot[xi,xj];
bj=b-Error[[j]]+(alphaj-alphajnew)*yj*Dot[xj,xj]+(alphai-alphainew)*yi*Dot[xi,xj];
omega=omega+(alphainew-alphai)*yi*xi+(alphajnew-alphaj)*yj*xj;
si=Select[Table[t,{t,1,Length[d]}],Alpha[[#]]>0&];
(*b=If[Length[si]==0,(bi+bj)/2,Mean[Map[d[[#,2]]-Dot[omega,d[[#,1]]]&,si]]];*)
b=(bi+bj)/2;
Alpha[[i]]=alphainew;Alpha[[j]]=alphajnew;
Error=Map[F[#[[1]]]-#[[2]]&,d];
If[Norm[omega]!=0,AppendTo[gamma,2/Norm[omega]]]
]


Do[Table[j=RandomInteger[{1,Length[d]}];UpdateSVM[i,j],{i,1,Length[d]}],100]
