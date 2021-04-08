import Persondecreeoperation from '../../classes/persondecreeoperation'
import Persondecreeblock from '../../classes/persondecreeblock'
import Persondecreeblocktype from '../../classes/persondecreeblocktype'
import Persondecreeblocksub from '../../classes/persondecreeblocksub'
import Persondecreeblocksubtype from '../../classes/persondecreeblocksubtype'
import User from '../../classes/user';
import Persondecree from '../../classes/persondecree';

export default class ExcertComposition {
    decree: Persondecree;
    decreeblocks: Persondecreeblock[];
    decreeblocksubs: Persondecreeblocksub[];
    decreeblocksubtypes: Persondecreeblocksubtype[];
    decreeblocktypes: Persondecreeblocktype[];
    decreeoperations: Persondecreeoperation[];
}