const webpack = require('webpack')

module.exports = {
    resolve: {
        fallback: {
            "path": false,
            "os": false,
            "crypto": false
        }
    },
    plugins: [
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV),
            'process.env.DB_USERNAME': JSON.stringify(process.env.DB_USERNAME),
            'process.env.DB_PASSWORD': JSON.stringify(process.env.DB_PASSWORD),
        })
    ]
}