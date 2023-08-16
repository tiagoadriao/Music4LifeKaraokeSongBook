(()=>{"use strict";function e(e,s){return Array.isArray(s)?function(e,s){let n=0;return e.replace(/\?/g,(e=>n<s.length?t(s[n++]):e))}(e,s):function(e,s){return e.replace(/:(\w+)/g,((e,n)=>function(e,t){return Object.prototype.hasOwnProperty.call(e,t)}(s,n)?t(s[n]):e))}(e,s)}function t(e){return null==e?"null":"number"==typeof e?String(e):"boolean"==typeof e?e?"true":"false":"string"==typeof e?s(e):Array.isArray(e)?e.map(t).join(", "):e instanceof Date?s(e.toISOString().replace("Z","")):s(e.toString())}function s(e){return`'${function(e){return e.replace(n,r)}(e)}'`}const n=/[\0\b\n\r\t\x1a\\"']/g;function r(e){switch(e){case'"':return'\\"';case"'":return"\\'";case"\n":return"\\n";case"\r":return"\\r";case"\t":return"\\t";case"\\":return"\\\\";case"\0":return"\\0";case"\b":return"\\b";case"":return"\\Z";default:return""}}const a=new TextDecoder("utf-8");function c(e){return e?a.decode(Uint8Array.from(function(e){return e.split("").map((e=>e.charCodeAt(0)))}(e))):""}class o extends Error{constructor(e,t,s){super(e),this.status=t,this.name="DatabaseError",this.body=s}}const i={as:"object"};class u{constructor(e){this.conn=e}async execute(e,t=null,s=i){return this.conn.execute(e,t,s)}}class f{constructor(e){var t;if(this.session=null,this.config={...e},"undefined"!=typeof fetch&&((t=this.config).fetch||(t.fetch=fetch)),e.url){const t=new URL(e.url);this.config.username=t.username,this.config.password=t.password,this.config.host=t.hostname}}async transaction(e){const t=new f(this.config),s=new u(t);try{await s.execute("BEGIN");const t=await e(s);return await s.execute("COMMIT"),t}catch(e){throw await s.execute("ROLLBACK"),e}}async refresh(){await this.createSession()}async execute(t,s=null,n=i){const r=new URL("/psdb.v1alpha1.Database/Execute",`https://${this.config.host}`),a=this.config.format||e,c=s?a(t,s):t,u=await h(this.config,r,{query:c,session:this.session}),{result:f,session:w,error:d,timing:y}=u;if(w&&(this.session=w),d)throw new o(d.message,400,d);const g=f?.rowsAffected?parseInt(f.rowsAffected,10):0,m=f?.insertId??"0",I=f?.fields??[];for(const e of I)e.type||(e.type="NULL");const T=n.cast||this.config.cast||p,A=f?function(e,t,s){const n=e.fields;return(e.rows??[]).map((e=>"array"===s?function(e,t,s){const n=l(t);return e.map(((e,t)=>s(e,n[t])))}(n,e,t):function(e,t,s){const n=l(t);return e.reduce(((e,t,r)=>(e[t.name]=s(t,n[r]),e)),{})}(n,e,t)))}(f,T,n.as||"object"):[],b=y??0;return{headers:I.map((e=>e.name)),types:I.reduce(((e,{name:t,type:s})=>({...e,[t]:s})),{}),fields:I,rows:A,rowsAffected:g,insertId:m,size:A.length,statement:c,time:1e3*b}}async createSession(){const e=new URL("/psdb.v1alpha1.Database/CreateSession",`https://${this.config.host}`),{session:t}=await h(this.config,e);return this.session=t,t}}async function h(e,t,s={}){const n=btoa(`${e.username}:${e.password}`),{fetch:r}=e,a=await r(t.toString(),{method:"POST",body:JSON.stringify(s),headers:{"Content-Type":"application/json","User-Agent":"database-js/1.10.0",Authorization:`Basic ${n}`},cache:"no-store"});if(a.ok)return await a.json();{let e=null;try{const t=(await a.json()).error;e=new o(t.message,a.status,t)}catch{e=new o(a.statusText,a.status,{code:"internal",message:a.statusText})}throw e}}function l(e){const t=e.values?atob(e.values):"";let s=0;return e.lengths.map((e=>{const n=parseInt(e,10);if(n<0)return null;const r=t.substring(s,s+n);return s+=n,r}))}function p(e,t){if(""===t||null==t)return t;switch(e.type){case"INT8":case"INT16":case"INT24":case"INT32":case"UINT8":case"UINT16":case"UINT24":case"UINT32":case"YEAR":return parseInt(t,10);case"FLOAT32":case"FLOAT64":return parseFloat(t);case"DECIMAL":case"INT64":case"UINT64":case"DATE":case"TIME":case"DATETIME":case"TIMESTAMP":case"BLOB":case"BIT":case"VARBINARY":case"BINARY":case"GEOMETRY":return t;case"JSON":return JSON.parse(c(t));default:return c(t)}}const w=new f({host:"aws.connect.psdb.cloud",username:"g3kb8fk5js3e5cqpm3mt",password:"pscale_pw_6OpnrIrQxIwTVVjXShkRC5KEf9LCWFZnV8FmVpWrjxv"});window.QueryDB=async function(e){return(await w.execute(e)).rows}})();