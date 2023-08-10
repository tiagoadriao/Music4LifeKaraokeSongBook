import { connect } from '@planetscale/database'

const config = {
    host: 'aws.connect.psdb.cloud',

    username: 'wnaxb1e2ze14gim9y23y',

    password: 'pscale_pw_aHX8dyPLVTkk834Kico5S0X8siCVcei5dVmYoFmUXPA'
}

const conn = connect(config)

window.QueryDB = async function(query) {
    const results = await conn.execute(query)

    return results.rows;
}