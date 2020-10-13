import User from './user';

export default class Persondecree {
    persondecreeManagementStatus: number; // 1 - Создать новый проект приказа, 2 - отменить проект приказа, 3 - подписать проект приказа, 4 - распечатать приказ
                                          // 5 - обновить информацию проекта приказа (номер, дата), 6 - предоставить к общему доступу/закрыть общий доступ.
    id: number;
    datecreated: Date;
    datesigned: Date;
    creator: number;
    owner: number;
    name: string;
    nickname: string;
    number: string;
    numbertype: string;
    transfer: number;
    persondecreelevel: number;
    signed: number;
    mailexplorerid: number;

    creatorObject: User;

    creatorfolder: number;
    ownerfolder: number;
    accessforreading: string;

    // Только на стороне фронтэнда
    marked: boolean;

    getFIO: string;
    getPlace: string;
    getDate: string;
    getName: string;
    getNumber: string;
}