#include <RCSwitch.h>


// ============================ //
// PINS SECTION                 //
#define   p_led_wtr       2
#define   p_led_on      4

#define   p_wtr_blck         34

#define   p_tx        15
#define   p_rx        5

#define   p_btn_init  23
// PINS SECTION                 //
// ============================ //

// ============================ //
// MESSAGES SECTION             //
#define REQ 0
#define RES 1

#pragma pack(push, 1)
struct st_rtm
{
	uint16_t  id;
	uint8_t   type	: 1;
	uint8_t   init	: 1;
	uint8_t   wtr	: 1;
	uint8_t   r		: 5;
	uint8_t   crc;
};

struct st_rrm
{
	uint16_t  hid;
  uint16_t  rid;
};

union s_msg
{
  uint32_t  val;
  st_rtm    rtm;
  st_rrm    rrm;
};

#pragma pack(pop)
// MESSAGES SECTION             //
// ============================ //



// ============================ //
// OBJECTS                      //
RCSwitch o_tx = RCSwitch();
RCSwitch o_rx = RCSwitch();

s_msg smsg;
// OBJECTS                      //
// ============================ //


// ============================ //
// RAM
const uint16_t          c_id                = 200;
uint16_t  c_hid               = 0;

bool  is_closed  = false;
bool is_init_mode = false;

String _debug_msg;
// RAM
// ============================ //


void _setup();
void _setup_rx();
void _change_water_block();
bool _process_init_btn();
void _init();
void _send(bool, bool, bool);
uint8_t _calc_crc(uint32_t);


void setup()
{  
    _setup();

    _setup_rx();

    if (digitalRead(p_btn_init) == HIGH)
    {
        bool _is_init = _process_init_btn();
        
        if (_is_init)
        {
            _init();
        }
        else
        {
            _change_water_block();
        }
    }
}


void _setup()
{
  Serial.begin(115200);

  pinMode(p_led_on, OUTPUT);

  pinMode(p_wtr_blck, INPUT);
  pinMode(p_btn_init, INPUT_PULLUP);
}

void _setup_rx()
{
    o_rx.setProtocol(5);
    o_rx.setReceiveTolerance(60);
    o_rx.enableReceive(digitalPinToInterrupt(p_rx));
}

bool _process_init_btn()
{
    digitalWrite(p_led_on, HIGH);
    for (int8_t i = 0; i < 4; i++)
    {
        delay(100);
        digitalWrite(p_led_on, LOW);
        delay(900);
        digitalWrite(p_led_on, HIGH);

        if (digitalRead(p_btn_init) == LOW)
        {
            return false;
        }
    }

    return true;
}

void _init()
{
    _debug_msg = String(20) + ": Enter INIT... ";
    Serial.println(_debug_msg);  
    _send(REQ, 1,0,0);

    uint64_t start_time = millis();  
    uint64_t blink_time = millis();  
    bool _is_received = false;
    bool _led_on = false;    

    is_init_mode = true;
}

void _change_water_block()
{
  is_closed = !is_closed;
  digitalWrite(p_led_on, is_closed);
  digitalWrite(p_wtr_blck, is_closed);
  _send(RES, 0, is_closed);
}

void _send(bool _type, bool _init, bool _wtr)
{
    int64_t _start_time = millis();
    smsg.rtm.id    = c_id;
    smsg.rtm.type  = _type;
    smsg.rtm.init  = _init;
    smsg.rtm.wtr   = _wtr;
    smsg.rtm.crc   = _calc_crc(smsg.val);

    o_tx.setRepeatTransmit(10);
    o_tx.enableTransmit(p_tx);
    o_tx.send(smsg.val, 32);
    o_tx.disableTransmit();

    int64_t _end_time = millis();
    String _debug_msg = "SENT: [" + String(smsg.val, HEX) + "] by " + String(_end_time - _start_time);
    Serial.println(_debug_msg);
}

uint8_t _calc_crc(uint32_t _val)
{
  uint8_t crc = 0xff;
  uint8_t dat[4];

  dat[0] = (_val >> 24) & 0xff;
  dat[1] = (_val >> 16) & 0xff;
  dat[2] = (_val >> 8) & 0xff;
  dat[3] = _val & 0xff;

  for (int8_t i = 0; i < 4; i++)
  {
    crc ^= dat[i];
    for (int8_t j = 0; j < 8; j++)
    {
      if (crc & 0x80) 
      {
        crc = (crc << 1) ^ 0x31;
      }
      else 
      {
        crc <<= 1;
      }
    }
  }

  return crc;
}

void loop()
{
  if (is_init_mode)
  {
    static unsigned long lastBlink = 0;

    if (millis() - lastBlink >= 500)
    {
        lastBlink = millis();
        digitalWrite(p_led_on, !digitalRead(p_led_on));
    }
  }

    if (o_rx.available())
    {  
        smsg.val = o_rx.getReceivedValue();
        _debug_msg = String(200) + ": Received message: [" + String(smsg.val, HEX) + "]";
        Serial.println(_debug_msg);  

        if (smsg.rrm.rid == c_id)
        {
            _debug_msg = String(200) + ": Received message: [" + String(smsg.val, HEX) + "]";
            Serial.println(_debug_msg);

            if (is_init_mode)
            {
              c_hid = smsg.rrm.hid;
              is_init_mode = false;
            }
            else 
            {
              if (c_hid == smsg.rrm.hid)
              {
                  _change_water_block();
              }
            }
        }
        
        o_rx.resetAvailable();
    }
}
