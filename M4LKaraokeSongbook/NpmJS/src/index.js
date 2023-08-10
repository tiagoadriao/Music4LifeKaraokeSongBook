import { connect } from '@planetscale/database'

const config = {
    host: 'aws.connect.psdb.cloud',

    username: '1dh1x3lb1y718yw0pnw9',

    password: atob('cHNjYWxlX3B3X0ZXTVVuTzZkcjZKVFc0ZWlrUlpGeUZLSnNhTldxeUpuOUJYQ3RoQUJyVXo=')
}

const conn = connect(config)

window.QueryDB = async function(query) {
    const results = await conn.execute(query)

    return results.rows;
}