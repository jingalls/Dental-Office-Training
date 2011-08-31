<%@ Page Language="C#" MasterPageFile="~/DentalOfficeTraining.master" AutoEventWireup="true"
    CodeFile="AboutMe.aspx.cs" Inherits="AboutMe" Title="About Me" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
    function ShowText() {
        document.getElementById("dvAdditionalText").style.display = "block";
        document.getElementById("spShowText").style.display = "none";
    }

    function HideText() {
        document.getElementById("dvAdditionalText").style.display = "none";
        document.getElementById("spShowText").style.display = "block";
    }
</script>

    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    <div style="width: 100%;" class="PinkHeaderText">
        Mission Statement
    </div>
    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    <div style="padding: 5px; width: 100%;">
        <p>
            Over the last 20 years, I have come in contact with and maintained professional
            relationships with many dentists. What they have relayed to me is that they have
            a huge demand placed on them to find competent, skilled employees that are capable
            of performing the necessary dental skills required in a professional office environment.
            They have advised that at present, the majority of those applying for dental office
            positions are not adequately trained to meet their needs as employers.</p>
        <p>
            With the encouragement of many local dentists, I have opened a dental office training
            center, for the training and development of dental assistants and front-office personnel.
            This program provides intense, hands-on training, as well as the study of terminology
            and anatomy to compliment the learned skills.</p>
            
        <span id="spShowText" style="width: 100%; float: right; text-align: right;">
            <a id="lnkShowMore" onclick="javascript: ShowText();">More...</a>
        </span>
        
        <div id="dvAdditionalText" style="padding: 5px; clear: both; display: none; width: 100%;">
            <p>
                In addition to excellence in training, my program offers job placement assistance,
                résumé-building and dental software training. I invite local dentists to come and
                evaluate the performance of my students while they are in training, thus leading
                to interviews and possible employment following graduation. This helps to open the
                lines of communication between dentists and their great employee prospects.</p>
            <p>
                In dentistry the ability to climb higher is always an option. There are many career
                paths one can choose once the basic educational structure is established and experience
                is gained. Highly skilled and trained individuals will have opportunities to enjoy
                benefits such as 401(k) plans, medical and dental insurance, and bonuses. These
                are just a few of the benefits that many offices provide to their valued employees.</p>
            <p>
                According to the Department of Labor, 113,000 dental assistant positions will need
                to be filled between 2002 and 2012. This will place a great demand on the dental
                profession and provide great opportunity for those with the best training. In my
                long and varied experience I have seen many changes in the workplace. Additionally,
                I have researched the needs of dentists and what they desire in their employees
                by way of personal interviews. I have targeted the areas that need improvement and
                structured training to meet those needs. This provides confident, well-trained employees
                and satisfied employers.</p>
            <p>
                Sincerely,</p>
            <p>
                Lynn Uptgraft, R.D.H.</p>
                
            <span id="spHideText" style="width: 100%; float: right; text-align: right;">
                <a id="lnkShowLess" onclick="javascript: HideText();">Hide...</a>
            </span>
        </div>
    </div>
    
    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    <div style="width: 100%;" class="PinkHeaderText">
        Trainers
    </div>
    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    
    <br />
    <div style="float: left; text-align: center;">
        <asp:Image ID="imgPic" runat="Server" ImageUrl="images/summer class 09 016.jpg" Width="150" />
        <br />
        Lynn Uptgraft R.D.H.</div>
    <div style="float: right; padding-left: 5px;">
        <p>
            I am a licensed dental hygienist with over 20 years' experience in dental hygiene,
            dental assisting and office management.</p>
        <p>
            I graduated from dental assisting school in 1985, upon which I began working as
            a dental assistant and front office hygiene coordinator.</p>
        <p>
            Then I graduated from Indiana University School of Dentistry in 1990 with a dental
            hygiene degree. Since then, I have been a dental hygienist, working with various
            dental software, and establishing continuing care programs for new and developing
            offices, including scheduling and coordinating front and back office duties.</p>
        <p>
            Now I am providing a training program for you, which includes hands-on learning
            with the tools, equipment, and computer software to develop your skills for today's
            dentistry.</p>
    </div>
    <div style="clear: both; padding-top: 5px;">
        <p class="PinkHeaderText">
            EFFECTIVE TRAINING. REAL RESULTS</p>
    </div>
    <hr style="width: 100%; color: #8B8B8B; height: 1px;" />
    <br />
    <div style="float: left; text-align: center;">
        <asp:Image ID="imgDenise" runat="Server" ImageUrl="images/summer class 09 020.jpg" Width="150" />
        <br />
        Denise McDonald</div>
    <div style="float: right; padding-left: 5px; height: 250px;">
        <p>
            My name is name is Denise McDonald and I teach the Expanded Dental Assistant curriculum
            for Dental Office Training. I have 30 years of working experience in the field of
            dentistry. I attended J. Everett Light Career Center in 1974, where I received my
            Dental Assistant’s certificate.
        </p>
        <p>
            In 1980 I received my certification in Expanded Functions from I.U. School of Dentistry.
            I am currently working in the field and, continue to look for ways to further my
            knowledge in every aspect of dentistry.
        </p>
        <p>
            Teaching has given me the ability to give back to the dental community, to help
            produce well-educated and trained dental assistants.</p>
    </div>
</asp:Content>
