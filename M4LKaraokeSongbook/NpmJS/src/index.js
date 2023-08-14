import { connect } from '@planetscale/database'

const config = {
    host: process.env.DB_HOST,

    username: process.env.DB_USERNAME,

    password: process.env.DB_PASSWORD
}

const conn = connect(config)

window.QueryDB = async function(query) {
    const results = await conn.execute(query)

    return results.rows;
}
