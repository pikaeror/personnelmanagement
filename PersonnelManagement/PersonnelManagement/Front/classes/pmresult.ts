export default class Pmresult {
    count: number;
    countvar: number;
    ranksexpanded: number;
    ranks: string;
    ranksvar: string;
    maxcount: string; // 160 separated by , 
    absolutecount: string; // 180 separated by , 
    defaultcount: string; // 100, separated by ,
    defaultcountvar: string; // 100, separated by ,
    mincount: string; // 80, separated by ,
    uprank: string; // При наличии ученой степени=1:5,при наличии классности=1:10;2:20,
    uprankready: string;  // Из них при наличии ученой степени степени получить звание майор внутренней службы 10 ед., подполковник внутренней службы 20 ед. & ...
    civil: string; // Государственные служащие=1,Cлужащие=5
    uppedmap: string; // Карта: Название=Айди;На сколько ранков максимум доступен подъем.  При наличии классности=1;2
    uppedcount: string; //При наличии классности=1:100;2:120&При наличии ученой степени=1:5, 
    uprankunited: string; // Капитан внутренней службы:1:5;Майор внутренней службы:2:5;
    comefromunited: string; // Старший лейтенант внутренней службы:1:5;Капитан внутренней службы:2:5;
    sumunited: string; // 100:120:100,30:50:50,
    unitedlengthmax: string; // Наибольшая из длин uprankunited или comefromunited. 2,3,0,0
    file: Blob;
}