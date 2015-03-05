--
-- PostgreSQL database dump
--

-- Dumped from database version 9.2.4
-- Dumped by pg_dump version 9.2.2
-- Started on 2015-03-05 21:29:25 AEDT

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- TOC entry 2355 (class 1262 OID 17764)
-- Name: newtestdb; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE newtestdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_AU.UTF-8' LC_CTYPE = 'en_AU.UTF-8';


ALTER DATABASE newtestdb OWNER TO postgres;

\connect newtestdb

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- TOC entry 2356 (class 1262 OID 17764)
-- Dependencies: 2355
-- Name: newtestdb; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE newtestdb IS 'Revised table structure to make it easier to work with through code';


--
-- TOC entry 191 (class 3079 OID 11995)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2359 (class 0 OID 0)
-- Dependencies: 191
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 182 (class 1259 OID 18000)
-- Name: Areas; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Areas" (
    "AreaID" integer NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "ProductID" integer NOT NULL
);


ALTER TABLE public."Areas" OWNER TO postgres;

--
-- TOC entry 181 (class 1259 OID 17998)
-- Name: Areas_AreaID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Areas_AreaID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Areas_AreaID_seq" OWNER TO postgres;

--
-- TOC entry 2360 (class 0 OID 0)
-- Dependencies: 181
-- Name: Areas_AreaID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Areas_AreaID_seq" OWNED BY "Areas"."AreaID";


--
-- TOC entry 178 (class 1259 OID 17870)
-- Name: ConfigFields; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "ConfigFields" (
    "FieldName" text,
    "FieldType" text,
    "ConfigFieldID" integer NOT NULL,
    "ProductID" integer NOT NULL
);


ALTER TABLE public."ConfigFields" OWNER TO postgres;

--
-- TOC entry 180 (class 1259 OID 17935)
-- Name: ConfigFields_ConfigFieldID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "ConfigFields_ConfigFieldID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."ConfigFields_ConfigFieldID_seq" OWNER TO postgres;

--
-- TOC entry 2361 (class 0 OID 0)
-- Dependencies: 180
-- Name: ConfigFields_ConfigFieldID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "ConfigFields_ConfigFieldID_seq" OWNED BY "ConfigFields"."ConfigFieldID";


--
-- TOC entry 179 (class 1259 OID 17878)
-- Name: ConfigValues; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "ConfigValues" (
    "ConfigFieldID" integer NOT NULL,
    "Value" text,
    "ConfigID" integer NOT NULL,
    "ProductID" integer NOT NULL
);


ALTER TABLE public."ConfigValues" OWNER TO postgres;

--
-- TOC entry 177 (class 1259 OID 17861)
-- Name: Configs; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Configs" (
    "ConfigID" integer NOT NULL,
    "Name" text,
    "ProductID" integer
);


ALTER TABLE public."Configs" OWNER TO postgres;

--
-- TOC entry 176 (class 1259 OID 17859)
-- Name: Config_ConfigID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Config_ConfigID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Config_ConfigID_seq" OWNER TO postgres;

--
-- TOC entry 2362 (class 0 OID 0)
-- Dependencies: 176
-- Name: Config_ConfigID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Config_ConfigID_seq" OWNED BY "Configs"."ConfigID";


--
-- TOC entry 171 (class 1259 OID 17778)
-- Name: Products; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Products" (
    "ProductID" integer NOT NULL,
    "Name" text
);


ALTER TABLE public."Products" OWNER TO postgres;

--
-- TOC entry 170 (class 1259 OID 17776)
-- Name: Products_ProductID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Products_ProductID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Products_ProductID_seq" OWNER TO postgres;

--
-- TOC entry 2363 (class 0 OID 0)
-- Dependencies: 170
-- Name: Products_ProductID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Products_ProductID_seq" OWNED BY "Products"."ProductID";


--
-- TOC entry 189 (class 1259 OID 18385)
-- Name: ProfileData; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "ProfileData" (
    "pId" character(36) NOT NULL,
    "Profile" character(36) NOT NULL,
    "Name" character varying(255) NOT NULL,
    "ValueString" text,
    "ValueBinary" bytea
);


ALTER TABLE public."ProfileData" OWNER TO postgres;

--
-- TOC entry 188 (class 1259 OID 18369)
-- Name: Profiles; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Profiles" (
    "pId" character(36) NOT NULL,
    "Username" character varying(255) NOT NULL,
    "ApplicationName" character varying(255) NOT NULL,
    "IsAnonymous" boolean,
    "LastActivityDate" timestamp with time zone,
    "LastUpdatedDate" timestamp with time zone
);


ALTER TABLE public."Profiles" OWNER TO postgres;

--
-- TOC entry 186 (class 1259 OID 18343)
-- Name: Roles; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Roles" (
    "Rolename" character varying(255) NOT NULL,
    "ApplicationName" character varying(255) NOT NULL
);


ALTER TABLE public."Roles" OWNER TO postgres;

--
-- TOC entry 190 (class 1259 OID 18400)
-- Name: Sessions; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Sessions" (
    "SessionId" character varying(80) NOT NULL,
    "ApplicationName" character varying(255) NOT NULL,
    "Created" timestamp with time zone NOT NULL,
    "Expires" timestamp with time zone NOT NULL,
    "Timeout" integer NOT NULL,
    "Locked" boolean NOT NULL,
    "LockId" integer NOT NULL,
    "LockDate" timestamp with time zone NOT NULL,
    "Data" text,
    "Flags" integer NOT NULL
);


ALTER TABLE public."Sessions" OWNER TO postgres;

--
-- TOC entry 183 (class 1259 OID 18006)
-- Name: TestCaseAreas; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "TestCaseAreas" (
    "TestCaseID" integer NOT NULL,
    "AreaID" integer NOT NULL
);


ALTER TABLE public."TestCaseAreas" OWNER TO postgres;

--
-- TOC entry 184 (class 1259 OID 18219)
-- Name: TestCaseConfigs; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "TestCaseConfigs" (
    "TestCaseID" integer NOT NULL,
    "ConfigID" integer NOT NULL
);


ALTER TABLE public."TestCaseConfigs" OWNER TO postgres;

--
-- TOC entry 175 (class 1259 OID 17850)
-- Name: TestCaseResults; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "TestCaseResults" (
    "TestRunID" integer NOT NULL,
    "TestCaseID" integer NOT NULL,
    "Result" text,
    "Comment" text,
    "ConfigID" integer NOT NULL
);


ALTER TABLE public."TestCaseResults" OWNER TO postgres;

--
-- TOC entry 2364 (class 0 OID 0)
-- Dependencies: 175
-- Name: COLUMN "TestCaseResults"."Result"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "TestCaseResults"."Result" IS 'PASS / FAIL / etc';


--
-- TOC entry 173 (class 1259 OID 17804)
-- Name: TestCases; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "TestCases" (
    "TestCaseID" integer NOT NULL,
    "TestCaseCode" text,
    "Title" text,
    "Preconditions" text,
    "Steps" text,
    "Expected" text,
    "Priority" integer DEFAULT 1 NOT NULL,
    "Requirements" text,
    "ScriptName" text,
    "ProductID" integer
);


ALTER TABLE public."TestCases" OWNER TO postgres;

--
-- TOC entry 2365 (class 0 OID 0)
-- Dependencies: 173
-- Name: COLUMN "TestCases"."TestCaseCode"; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN "TestCases"."TestCaseCode" IS 'Unique ID or Code for a test case.';


--
-- TOC entry 174 (class 1259 OID 17807)
-- Name: TestCases_TestCaseID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "TestCases_TestCaseID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."TestCases_TestCaseID_seq" OWNER TO postgres;

--
-- TOC entry 2366 (class 0 OID 0)
-- Dependencies: 174
-- Name: TestCases_TestCaseID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "TestCases_TestCaseID_seq" OWNED BY "TestCases"."TestCaseID";


--
-- TOC entry 172 (class 1259 OID 17787)
-- Name: TestRunProducts; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "TestRunProducts" (
    "TestRunID" integer NOT NULL,
    "ProductID" integer NOT NULL,
    "Build" text
);


ALTER TABLE public."TestRunProducts" OWNER TO postgres;

--
-- TOC entry 169 (class 1259 OID 17767)
-- Name: TestRuns; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "TestRuns" (
    "TestRunID" integer NOT NULL,
    "RunDate" timestamp with time zone,
    "Comments" text,
    "ComputerName" text,
    "OS" text,
    "Language" text,
    "ConfigID" integer,
    "CpuSpeed" character varying(90) DEFAULT 'Not Recorded'::character varying,
    "TotalPhysicalMemory" character varying(90) DEFAULT 'Not Recorded'::character varying,
    "AvailablePhysicalMemory" character varying(90) DEFAULT 'Not Recorded'::character varying,
    "TotalVirtualMemory" character varying(90) DEFAULT 'Not Recorded'::character varying,
    "AvailableVirtualMemory" character varying(90) DEFAULT 'Not Recorded'::character varying
);


ALTER TABLE public."TestRuns" OWNER TO postgres;

--
-- TOC entry 168 (class 1259 OID 17765)
-- Name: TestRun_TestRunID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "TestRun_TestRunID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."TestRun_TestRunID_seq" OWNER TO postgres;

--
-- TOC entry 2367 (class 0 OID 0)
-- Dependencies: 168
-- Name: TestRun_TestRunID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "TestRun_TestRunID_seq" OWNED BY "TestRuns"."TestRunID";


--
-- TOC entry 185 (class 1259 OID 18331)
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "Users" (
    "pId" character(36) NOT NULL,
    "Username" character varying(255) NOT NULL,
    "ApplicationName" character varying(255) NOT NULL,
    "Email" character varying(128),
    "Comment" character varying(128),
    "Password" character varying(255) NOT NULL,
    "PasswordQuestion" character varying(255),
    "PasswordAnswer" character varying(255),
    "IsApproved" boolean,
    "LastActivityDate" timestamp with time zone,
    "LastLoginDate" timestamp with time zone,
    "LastPasswordChangedDate" timestamp with time zone,
    "CreationDate" timestamp with time zone,
    "IsOnLine" boolean,
    "IsLockedOut" boolean,
    "LastLockedOutDate" timestamp with time zone,
    "FailedPasswordAttemptCount" integer,
    "FailedPasswordAttemptWindowStart" timestamp with time zone,
    "FailedPasswordAnswerAttemptCount" integer,
    "FailedPasswordAnswerAttemptWindowStart" timestamp with time zone
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 187 (class 1259 OID 18351)
-- Name: UsersInRoles; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE "UsersInRoles" (
    "Username" character varying(255) NOT NULL,
    "Rolename" character varying(255) NOT NULL,
    "ApplicationName" character varying(255) NOT NULL
);


ALTER TABLE public."UsersInRoles" OWNER TO postgres;

--
-- TOC entry 2282 (class 2604 OID 18003)
-- Name: AreaID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Areas" ALTER COLUMN "AreaID" SET DEFAULT nextval('"Areas_AreaID_seq"'::regclass);


--
-- TOC entry 2281 (class 2604 OID 17937)
-- Name: ConfigFieldID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConfigFields" ALTER COLUMN "ConfigFieldID" SET DEFAULT nextval('"ConfigFields_ConfigFieldID_seq"'::regclass);


--
-- TOC entry 2280 (class 2604 OID 17864)
-- Name: ConfigID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Configs" ALTER COLUMN "ConfigID" SET DEFAULT nextval('"Config_ConfigID_seq"'::regclass);


--
-- TOC entry 2277 (class 2604 OID 17781)
-- Name: ProductID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Products" ALTER COLUMN "ProductID" SET DEFAULT nextval('"Products_ProductID_seq"'::regclass);


--
-- TOC entry 2278 (class 2604 OID 17809)
-- Name: TestCaseID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCases" ALTER COLUMN "TestCaseID" SET DEFAULT nextval('"TestCases_TestCaseID_seq"'::regclass);


--
-- TOC entry 2271 (class 2604 OID 17770)
-- Name: TestRunID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestRuns" ALTER COLUMN "TestRunID" SET DEFAULT nextval('"TestRun_TestRunID_seq"'::regclass);


--
-- TOC entry 2304 (class 2606 OID 18005)
-- Name: Areas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Areas"
    ADD CONSTRAINT "Areas_pkey" PRIMARY KEY ("AreaID");


--
-- TOC entry 2300 (class 2606 OID 18069)
-- Name: ConfigFields_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "ConfigFields"
    ADD CONSTRAINT "ConfigFields_pkey" PRIMARY KEY ("ConfigFieldID", "ProductID");


--
-- TOC entry 2302 (class 2606 OID 18076)
-- Name: ConfigValues_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "ConfigValues"
    ADD CONSTRAINT "ConfigValues_pkey" PRIMARY KEY ("ConfigFieldID", "ProductID", "ConfigID");


--
-- TOC entry 2296 (class 2606 OID 17869)
-- Name: Config_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Configs"
    ADD CONSTRAINT "Config_pkey" PRIMARY KEY ("ConfigID");


--
-- TOC entry 2298 (class 2606 OID 18208)
-- Name: Configs_ProductID_Name_key; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Configs"
    ADD CONSTRAINT "Configs_ProductID_Name_key" UNIQUE ("ProductID", "Name");


--
-- TOC entry 2294 (class 2606 OID 17857)
-- Name: Results_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "TestCaseResults"
    ADD CONSTRAINT "Results_pkey" PRIMARY KEY ("TestRunID", "TestCaseID", "ConfigID");


--
-- TOC entry 2306 (class 2606 OID 18010)
-- Name: TestCaseAreas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "TestCaseAreas"
    ADD CONSTRAINT "TestCaseAreas_pkey" PRIMARY KEY ("TestCaseID", "AreaID");


--
-- TOC entry 2309 (class 2606 OID 18223)
-- Name: TestCaseConfigs_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "TestCaseConfigs"
    ADD CONSTRAINT "TestCaseConfigs_pkey" PRIMARY KEY ("TestCaseID", "ConfigID");


--
-- TOC entry 2292 (class 2606 OID 17817)
-- Name: TestCases_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "TestCases"
    ADD CONSTRAINT "TestCases_pkey" PRIMARY KEY ("TestCaseID");


--
-- TOC entry 2288 (class 2606 OID 17803)
-- Name: TestRunProducts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "TestRunProducts"
    ADD CONSTRAINT "TestRunProducts_pkey" PRIMARY KEY ("TestRunID", "ProductID");


--
-- TOC entry 2286 (class 2606 OID 17786)
-- Name: product_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Products"
    ADD CONSTRAINT product_pkey PRIMARY KEY ("ProductID");


--
-- TOC entry 2326 (class 2606 OID 18392)
-- Name: profiledata_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "ProfileData"
    ADD CONSTRAINT profiledata_pkey PRIMARY KEY ("pId");


--
-- TOC entry 2328 (class 2606 OID 18394)
-- Name: profiledata_profile_name_unique; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "ProfileData"
    ADD CONSTRAINT profiledata_profile_name_unique UNIQUE ("Profile", "Name");


--
-- TOC entry 2322 (class 2606 OID 18376)
-- Name: profiles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Profiles"
    ADD CONSTRAINT profiles_pkey PRIMARY KEY ("pId");


--
-- TOC entry 2324 (class 2606 OID 18378)
-- Name: profiles_username_application_unique; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Profiles"
    ADD CONSTRAINT profiles_username_application_unique UNIQUE ("Username", "ApplicationName");


--
-- TOC entry 2317 (class 2606 OID 18350)
-- Name: roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Roles"
    ADD CONSTRAINT roles_pkey PRIMARY KEY ("Rolename", "ApplicationName");


--
-- TOC entry 2330 (class 2606 OID 18407)
-- Name: sessions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Sessions"
    ADD CONSTRAINT sessions_pkey PRIMARY KEY ("SessionId", "ApplicationName");


--
-- TOC entry 2284 (class 2606 OID 17772)
-- Name: testrun_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "TestRuns"
    ADD CONSTRAINT testrun_pkey PRIMARY KEY ("TestRunID");


--
-- TOC entry 2313 (class 2606 OID 18338)
-- Name: users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Users"
    ADD CONSTRAINT users_pkey PRIMARY KEY ("pId");


--
-- TOC entry 2315 (class 2606 OID 18340)
-- Name: users_username_application_unique; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "Users"
    ADD CONSTRAINT users_username_application_unique UNIQUE ("Username", "ApplicationName");


--
-- TOC entry 2319 (class 2606 OID 18358)
-- Name: usersinroles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY "UsersInRoles"
    ADD CONSTRAINT usersinroles_pkey PRIMARY KEY ("Username", "Rolename", "ApplicationName");


--
-- TOC entry 2289 (class 1259 OID 17801)
-- Name: fki_products_fkey; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX fki_products_fkey ON "TestRunProducts" USING btree ("ProductID");


--
-- TOC entry 2307 (class 1259 OID 18021)
-- Name: fki_testcaseareas_area_fkey; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX fki_testcaseareas_area_fkey ON "TestCaseAreas" USING btree ("AreaID");


--
-- TOC entry 2290 (class 1259 OID 17795)
-- Name: fki_testrun_fkey; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX fki_testrun_fkey ON "TestRunProducts" USING btree ("TestRunID");


--
-- TOC entry 2320 (class 1259 OID 18384)
-- Name: profiles_isanonymous_index; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX profiles_isanonymous_index ON "Profiles" USING btree ("IsAnonymous");


--
-- TOC entry 2310 (class 1259 OID 18341)
-- Name: users_email_index; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX users_email_index ON "Users" USING btree ("Email");


--
-- TOC entry 2311 (class 1259 OID 18342)
-- Name: users_islockedout_index; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX users_islockedout_index ON "Users" USING btree ("IsLockedOut");


--
-- TOC entry 2342 (class 2606 OID 18214)
-- Name: Areas_ProductID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Areas"
    ADD CONSTRAINT "Areas_ProductID_fkey" FOREIGN KEY ("ProductID") REFERENCES "Products"("ProductID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2339 (class 2606 OID 18112)
-- Name: ConfigFields_ProductID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConfigFields"
    ADD CONSTRAINT "ConfigFields_ProductID_fkey" FOREIGN KEY ("ProductID") REFERENCES "Products"("ProductID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2341 (class 2606 OID 18127)
-- Name: ConfigValues_ConfigFieldID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConfigValues"
    ADD CONSTRAINT "ConfigValues_ConfigFieldID_fkey" FOREIGN KEY ("ConfigFieldID", "ProductID") REFERENCES "ConfigFields"("ConfigFieldID", "ProductID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2340 (class 2606 OID 18122)
-- Name: ConfigValues_ConfigID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConfigValues"
    ADD CONSTRAINT "ConfigValues_ConfigID_fkey" FOREIGN KEY ("ConfigID") REFERENCES "Configs"("ConfigID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2338 (class 2606 OID 18202)
-- Name: Configs_ProductID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Configs"
    ADD CONSTRAINT "Configs_ProductID_fkey" FOREIGN KEY ("ProductID") REFERENCES "Products"("ProductID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2344 (class 2606 OID 18152)
-- Name: TestCaseAreas_AreaID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseAreas"
    ADD CONSTRAINT "TestCaseAreas_AreaID_fkey" FOREIGN KEY ("AreaID") REFERENCES "Areas"("AreaID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2343 (class 2606 OID 18147)
-- Name: TestCaseAreas_TestCaseID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseAreas"
    ADD CONSTRAINT "TestCaseAreas_TestCaseID_fkey" FOREIGN KEY ("TestCaseID") REFERENCES "TestCases"("TestCaseID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2345 (class 2606 OID 18244)
-- Name: TestCaseConfigs_ConfigID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseConfigs"
    ADD CONSTRAINT "TestCaseConfigs_ConfigID_fkey" FOREIGN KEY ("ConfigID") REFERENCES "Configs"("ConfigID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2346 (class 2606 OID 18249)
-- Name: TestCaseConfigs_TestCaseID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseConfigs"
    ADD CONSTRAINT "TestCaseConfigs_TestCaseID_fkey" FOREIGN KEY ("TestCaseID") REFERENCES "TestCases"("TestCaseID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2336 (class 2606 OID 18177)
-- Name: TestCaseResults_ConfigID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseResults"
    ADD CONSTRAINT "TestCaseResults_ConfigID_fkey" FOREIGN KEY ("ConfigID") REFERENCES "Configs"("ConfigID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2337 (class 2606 OID 18182)
-- Name: TestCaseResults_TestCaseID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseResults"
    ADD CONSTRAINT "TestCaseResults_TestCaseID_fkey" FOREIGN KEY ("TestCaseID") REFERENCES "TestCases"("TestCaseID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2335 (class 2606 OID 18172)
-- Name: TestCaseResults_TestRunID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCaseResults"
    ADD CONSTRAINT "TestCaseResults_TestRunID_fkey" FOREIGN KEY ("TestRunID") REFERENCES "TestRuns"("TestRunID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2334 (class 2606 OID 18187)
-- Name: TestCases_ProductID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestCases"
    ADD CONSTRAINT "TestCases_ProductID_fkey" FOREIGN KEY ("ProductID") REFERENCES "Products"("ProductID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2332 (class 2606 OID 18192)
-- Name: TestRunProducts_ProductID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestRunProducts"
    ADD CONSTRAINT "TestRunProducts_ProductID_fkey" FOREIGN KEY ("ProductID") REFERENCES "Products"("ProductID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2333 (class 2606 OID 18197)
-- Name: TestRunProducts_TestRunID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestRunProducts"
    ADD CONSTRAINT "TestRunProducts_TestRunID_fkey" FOREIGN KEY ("TestRunID") REFERENCES "TestRuns"("TestRunID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2331 (class 2606 OID 18326)
-- Name: TestRuns_ConfigID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "TestRuns"
    ADD CONSTRAINT "TestRuns_ConfigID_fkey" FOREIGN KEY ("ConfigID") REFERENCES "Configs"("ConfigID") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 2350 (class 2606 OID 18409)
-- Name: profiledata_profile_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ProfileData"
    ADD CONSTRAINT profiledata_profile_fkey FOREIGN KEY ("Profile") REFERENCES "Profiles"("pId") ON DELETE CASCADE;


--
-- TOC entry 2349 (class 2606 OID 18414)
-- Name: profiles_username_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Profiles"
    ADD CONSTRAINT profiles_username_fkey FOREIGN KEY ("Username", "ApplicationName") REFERENCES "Users"("Username", "ApplicationName") ON DELETE CASCADE;


--
-- TOC entry 2347 (class 2606 OID 18419)
-- Name: usersinroles_rolename_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "UsersInRoles"
    ADD CONSTRAINT usersinroles_rolename_fkey FOREIGN KEY ("Rolename", "ApplicationName") REFERENCES "Roles"("Rolename", "ApplicationName") ON DELETE CASCADE;


--
-- TOC entry 2348 (class 2606 OID 18424)
-- Name: usersinroles_username_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "UsersInRoles"
    ADD CONSTRAINT usersinroles_username_fkey FOREIGN KEY ("Username", "ApplicationName") REFERENCES "Users"("Username", "ApplicationName") ON DELETE CASCADE;


--
-- TOC entry 2358 (class 0 OID 0)
-- Dependencies: 5
-- Name: public; Type: ACL; Schema: -; Owner: pete
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM pete;
GRANT ALL ON SCHEMA public TO pete;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2015-03-05 21:29:25 AEDT

--
-- PostgreSQL database dump complete
--

